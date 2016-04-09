using System;

namespace JsonNetDiscriminator
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class JsonPropertyDiscriminatorAttribute : Attribute
    {
        private string _propertyName;
        private string _propertyValue;
        private Type _targetType;

        public string PropertyName
        {
            get { return _propertyName; }
        }

        public string PropertyValue
        {
            get { return _propertyValue; }
        }

        public Type TargetType
        {
            get { return _targetType; }
        }

        public JsonPropertyDiscriminatorAttribute(string propertyName, string propertyValue, Type targetType)
        {
            _propertyName = propertyName;
            _propertyValue = propertyValue;
            _targetType = targetType;
        }
    }
}