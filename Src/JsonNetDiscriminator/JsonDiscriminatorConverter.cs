using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace JsonNetDiscriminator
{
    public class JsonDiscriminatorConverter : JsonConverter
    {
        private List<Type> _types;

        internal List<Type> Types
        {
            get { return _types; }
        }

        public JsonDiscriminatorConverter()
        {
            _types = new List<Type>();
        }
        public JsonDiscriminatorConverter(params Type[] types)
            :this()
        {
            foreach (var type in types)
                AddType(type);
        }
        public JsonDiscriminatorConverter(params Assembly[] assemblies)
            :this()
        {
            foreach (var assembly in assemblies)
                ScanAssembly(assembly);
        }

        public void AddType(Type type)
        {
            if (!_types.Contains(type))
                _types.Add(type);
        }
        public void RemoveType(Type type)
        {
            _types.Remove(type);
        }

        public void ScanAssembly(Assembly assembly)
        {
            _types.AddRange(
                assembly.GetTypes()
                    .Where(x => x.IsClass && x.IsPublic && x.GetCustomAttributes(typeof(JsonPropertyDiscriminatorAttribute), false).Any())
                    .ToList());
        }


        public override bool CanWrite
        {
            get { return false; }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        private object Create(Type objectType, JObject jsonObject)
        {
            var attrs = objectType.GetCustomAttributes(typeof (JsonPropertyDiscriminatorAttribute), false).OfType<JsonPropertyDiscriminatorAttribute>().ToList();
            if (attrs.Any())
            {
                JToken value = null;
                foreach (var attr in attrs)
                {
                    if (jsonObject.TryGetValue(attr.PropertyName, out value) && ((string)value).Equals(attr.PropertyValue))
                        return Activator.CreateInstance(attr.TargetType);
                }
            }
            return Activator.CreateInstance(objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return _types != null && _types.Any(x => x.IsAssignableFrom(objectType));
        }
    }
}