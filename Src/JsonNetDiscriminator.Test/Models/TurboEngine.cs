using Newtonsoft.Json;

namespace JsonNetDiscriminator.Test.Models
{
    public class TurboEngine : Engine
    {
        [JsonProperty("acceleration")]
        public decimal? Acceleration { get; set; }
    }
}
