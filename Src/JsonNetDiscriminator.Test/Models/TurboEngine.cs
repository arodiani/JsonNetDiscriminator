using Newtonsoft.Json;

namespace JsonNetDiscriminator.Test.Models
{
    public class TurboEngine : Engine
    {
        [JsonProperty("acc")]
        public decimal Acceleration { get; set; }
    }
}
