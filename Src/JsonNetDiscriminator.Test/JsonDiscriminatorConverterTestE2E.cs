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
        public void Test_Vehicle_JsonWithOnlyVehicleProperties()
        {
            string json = "{\"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\"}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof (Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_Vehicle_JsonWithVanProperties_NoConverterAndNoDiscriminator()
        {
            string json = "{\"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_Vehicle_JsonWithVanPropertiesAndVanType_NoConverterAndWithDiscriminator()
        {
            string json = "{\"type\": \"Van\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_Vehicle_JsonWithVanProperties_WithConverterAndNoDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_Vehicle_JsonWithVanPropertiesAndVanType_WithConverterAndWithDiscriminator()
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
        public void Test_Vehicle_JsonWithVanProperties_WithConverterAndWithInvalidDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"VanCool\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Vehicle));
            //vehicle.Should().NotBeOfType(typeof(Vehicle));
        }

        [Test]
        public void Test_Vehicle_JsonWithVanPropertiesButCarType_NoConverterAndWithDiscriminator()
        {
            string json = "{\"type\": \"Car\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);

            vehicle.Should().BeOfType(typeof(Vehicle));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
        }

        [Test]
        public void Test_Vehicle_JsonWithVanPropertiesButCarType_WithConverterAndWithDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"Car\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"carryingCapacity\": 7.35}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Car));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
            ((Car) vehicle).NumberOfDoors.Should().Be(null);
        }


        [Test]
        public void Test_Vehicle_JsonWithCarPropertiesAndCarType_WithConverterAndWithDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"Car\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"ndoors\": 5}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Car));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
            ((Car)vehicle).NumberOfDoors.Should().Be(5);
        }


        [Test]
        public void Test_VehicleNested_JsonWithCarTypeAndProperties_JsonWithEngine_NoConverterAndNoDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"Car\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"ndoors\": 5, \"engine\": {\"power\": 100}}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Car));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
            ((Car) vehicle).NumberOfDoors.Should().Be(5);
            ((Car) vehicle).CarEngine.Should().NotBeNull();
            ((Car) vehicle).CarEngine.Should().BeOfType(typeof (Engine));
            ((Car) vehicle).CarEngine.Power.Should().Be(100m);
        }

        [Test]
        public void Test_VehicleNested_JsonWithCarTypeAndProperties_JsonWithTurbeEngineType_NoConverterAndWithDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle));

            string json = "{\"type\": \"Car\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"ndoors\": 5, \"engine\": {\"type\": \"Turbo\", \"power\": 100, \"acceleration\": 3.6}}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Car));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
            ((Car)vehicle).NumberOfDoors.Should().Be(5);
            ((Car)vehicle).CarEngine.Should().NotBeNull();
            ((Car)vehicle).CarEngine.Should().BeOfType(typeof(Engine));
        }

        [Test]
        public void Test_VehicleNested_JsonWithCarTypeAndProperties_JsonWithTurbeEngineType_WithConverterAndWithDiscriminator()
        {
            JsonDiscriminatorConverter converter = new JsonDiscriminatorConverter(typeof(Vehicle), typeof(Engine));

            string json = "{\"type\": \"Car\", \"color\": \"red\", \"dateOfRegistration\": \"2016-04-09\", \"ndoors\": 5, \"engine\": {\"type\": \"Turbo\", \"power\": 100, \"acceleration\": 3.6}}";

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(json, converter);

            vehicle.Should().BeOfType(typeof(Car));
            vehicle.Color.Should().BeEquivalentTo("red");
            vehicle.DateOfRegistration.Should().BeEquivalentTo("2016-04-09");
            ((Car)vehicle).NumberOfDoors.Should().Be(5);
            ((Car)vehicle).CarEngine.Should().NotBeNull();
            ((Car)vehicle).CarEngine.Should().BeOfType(typeof(TurboEngine));

            TurboEngine engine = (TurboEngine)((Car) vehicle).CarEngine;
            engine.Power.Should().Be(100m);
            engine.Acceleration = 3.6m;
        }

    }
}