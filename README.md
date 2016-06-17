# JsonNetDiscriminator

Lastest Version *0.0.1.375*

### What is JsonNetDiscriminator

This is a Newtonsoft JSON.NET extension that helps to face inheritance problems during JSON deserialization.

Suppose to have the following models.

    public class Vehicle
    {
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "dateOfRegistration")]
        public string DateOfRegistration { get; set; }
    }
    
    public class Van : Vehicle
    {
        [JsonProperty(PropertyName = "carryingCapacity")]
        public decimal? CarryingCapacity { get; set; }
    }
    
    public class Car : Vehicle
    {
        [JsonProperty(PropertyName = "ndoors")]
        public int? NumberOfDoors { get; set; }

        [JsonProperty(PropertyName = "engine")]
        public Engine CarEngine { get; set; }
    }
    
Thanks to inheritance, in C# we don't have to declare a property that hold the information about the type of the object. 
Unluckly, JSON object declaration isn't as strict as C# one.

Now suppose that you have to deserialize the following JSON:

{
	"color": "red",
	"dateOfRegistration": "2016-04-09",
}

Looking at the object properties, you can infer that it could be deserialize into a Vehicle object.

And now?

{
	"color": "red",
	"dateOfRegistration": "2016-04-09",
	"ndoors": 5
}

As we can see, into json there is the property *ndoors* which let us think about Car object but this isn't true into many other situations.

To have the certainty that we are speaking about a Car object, a discriminator property have to be added.

{
	"type": "Car",
	"color": "red",
	"dateOfRegistration": "2016-04-09",
	"ndoors": 5
}

In this case, thanks to *type* property, we are sure that this JSON can be deserialize into a Car object.


And now **JsonNetDiscriminator** comes in handy.

You simply have to enrich Vehicle class with JsonPropertyDiscriminator attribute with the right matching types

    [JsonPropertyDiscriminator("type", "Car", typeof(Car))]
    [JsonPropertyDiscriminator("type", "Van", typeof(Van))]
    public class Vehicle
    {
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "dateOfRegistration")]
        public string DateOfRegistration { get; set; }
    }
    
In this case we are saying that if the JSON contains the *type* property with its value *Car*, this JSON can be deserialize into a Car object. If the value is *Van*, into a Van object.

Calling JSON deserialization method

    var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, new JsonDiscriminatorConverter(typeof(Vehicle)));
    
*vehicle* will be a Car object.