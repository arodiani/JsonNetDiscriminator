using Newtonsoft.Json;

namespace JsonNetDiscriminator.Test.Models
{
    public class Car : Vehicle
    {
        [JsonProperty(PropertyName = "ndoors")]
        public int? NumberOfDoors { get; set; }

        [JsonProperty(PropertyName = "engine")]
        public Engine CarEngine { get; set; }
    }
}
