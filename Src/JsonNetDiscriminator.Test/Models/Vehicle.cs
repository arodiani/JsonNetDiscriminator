using JsonNetDiscriminator;
using Newtonsoft.Json;

namespace JsonNetDiscriminator.Test.Models
{
    [JsonPropertyDiscriminator("type", "Car", typeof(Car))]
    [JsonPropertyDiscriminator("type", "Van", typeof(Van))]
    public class Vehicle
    {
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "dateOfRegistration")]
        public string DateOfRegistration { get; set; }
    }
}
