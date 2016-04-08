using System.Reflection;
using FluentAssertions;
using JsonNetDiscriminator.Test.Models;
using NUnit.Framework;

namespace JsonNetDiscriminator.Test
{
    // In this test class, I'll test only methods that help to manage list of types
    // For End-To-End tests, check JsonDiscriminatorConverterTestE2E.cs

    [TestFixture]
    public class JsonDiscriminatorConverterTest
    {
        private JsonDiscriminatorConverter _sut;

        [SetUp]
        public void InitSut()
        {
            _sut = new JsonDiscriminatorConverter();
        }




        [Test]
        public void Constructor_AddOneType()
        {
            _sut = new JsonDiscriminatorConverter(typeof(Vehicle));

            _sut.Types.Should().HaveCount(1);
        }

        [Test]
        public void Constructor_AddTwoDifferentTypes()
        {
            _sut = new JsonDiscriminatorConverter(typeof(Vehicle), typeof(Engine));

            _sut.Types.Should().HaveCount(2);
        }

        [Test]
        public void Constructor_AddTwoEqualTypesAndOnlyOneInList()
        {
            _sut = new JsonDiscriminatorConverter(typeof(Vehicle), typeof(Vehicle));

            _sut.Types.Should().HaveCount(1);
        }

        [Test]
        public void Constructor_AddOneTypeScanningTheCurrentTestAssembly()
        {
            _sut = new JsonDiscriminatorConverter(Assembly.GetExecutingAssembly());

            _sut.Types.Should().HaveCount(2);
        }


        [Test]
        public void AddType_AddOneType()
        {
            _sut.AddType(typeof(Vehicle));

            _sut.Types.Should().HaveCount(1);
        }

        [Test]
        public void AddType_AddTwoDifferentTypes()
        {
            _sut.AddType(typeof(Vehicle));
            _sut.AddType(typeof(Engine));

            _sut.Types.Should().HaveCount(2);
        }

        [Test]
        public void AddType_AddTwoEqualTypesAndOnlyOneInList()
        {
            _sut.AddType(typeof(Vehicle));
            _sut.AddType(typeof(Vehicle));

            _sut.Types.Should().HaveCount(1);
        }

        [Test]
        public void AddType_AddOneTypeAndRemoveIt()
        {
            _sut.AddType(typeof(Vehicle));
            _sut.RemoveType(typeof(Vehicle));

            _sut.Types.Should().HaveCount(0);
        }

        [Test]
        public void AddType_AddOneTypeScanningTheCurrentTestAssembly()
        {
            _sut.ScanAssembly(Assembly.GetExecutingAssembly());

            _sut.Types.Should().HaveCount(2);
        }
    }
}
