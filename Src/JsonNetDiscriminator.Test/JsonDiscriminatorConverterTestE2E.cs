using FluentAssertions;
using JsonNetDiscriminator.Test.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JsonNetDiscriminator.Test
{
    [TestFixture]
    public class JsonDiscriminatorConverterTestE2E
    {

        [Test]
        public void Test_001_JsonWithOnlyVehicleProperties()
        {
            string json = "{\"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\"}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof (Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }


        [Test]
        public void Test_002_JsonWithVanPropertiesAndNoConverterAndNoDiscriminator()
        {
            string json = "{\"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_003_JsonWithVanPropertiesAndNoConverterAndWithDiscriminator()
        {
            string json = "{\"type\": \"Van\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_004_JsonWithVanPropertiesAndWithConverterAndNoDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_005_JsonWithVanPropertiesAndWithConverterAndWithDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"Van\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Van));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
            ((Van) vehicle).CarryingCapacity.Should().Be(7.35m);
        }

        [Test]
        [Ignore("NotBeOfType assertion is missing atm")]
        public void Test_005_JsonWithVanPropertiesAndWithConverterAndWithInvalidDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"VanCool\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Vehicle));
            //vehicle.Should().NotBeOfType(typeof(Vehicle));
        }

    }
}