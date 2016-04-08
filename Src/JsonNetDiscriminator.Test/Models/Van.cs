using Newtonsoft.Json;

namespace JsonNetDiscriminator.Test.Models
{
    public class Van : Vehicle
    {
        [JsonProperty(PropertyName = "carryingCapacity")]
        public decimal CarryingCapacity { get; set; }
    }
}
