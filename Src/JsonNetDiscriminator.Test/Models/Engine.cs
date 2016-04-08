using JsonNetDiscriminator;
using Newtonsoft.Json;

namespace JsonNetDiscriminator.Test.Models
{
    [JsonPropertyDiscriminator("type", "Normal", typeof(NormalEngine))]
    [JsonPropertyDiscriminator("type", "Turbo", typeof(TurboEngine))]
    public class Engine
    {
        [JsonProperty("power")]
        public decimal Power { get; set; }
    }
}
