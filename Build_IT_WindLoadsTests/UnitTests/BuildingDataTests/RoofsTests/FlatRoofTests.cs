using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.BuildingData.Walls;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests.RoofsTests
{
    [TestFixture]
    public class FlatRoofTests
    {
        [Test]
        public void ConstructorTest_Roof_Success()
        {
            var building = new FlatRoof(length: 10, width: 5, height: 3);

            Assert.That(building.Length, Is.EqualTo(10));
            Assert.That(building.Width, Is.EqualTo(5));
            Assert.That(building.Height, Is.EqualTo(3));
            Assert.That(building.Areas.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ConstructorTest_Roof_Rotated_Success()
        {
            var building = new FlatRoof(length: 10, width: 5, height: 3, 
                FlatRoof.Rotation.Degrees_90);

            Assert.That(building.Length, Is.EqualTo(5));
            Assert.That(building.Width, Is.EqualTo(10));
            Assert.That(building.Height, Is.EqualTo(3));
            Assert.That(building.Areas.Count, Is.GreaterThan(0));
        }

        [Test]
        [TestCase(3, 5)]
        [TestCase(1, 2)]
        public void EdgeDistanceTest_Roof_Success(double height, double expectedResult)
        {
            var building = new FlatRoof(length: 10, width: 5, height: height);

            Assert.That(building.EdgeDistance, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AreasTest_Roof_Success()
        {
            var building = new FlatRoof(length: 10, width: 15, height: 5);

            Assert.That(building.Areas[Field.F], Is.EqualTo(2.5));
            Assert.That(building.Areas[Field.G], Is.EqualTo(10));
            Assert.That(building.Areas[Field.H], Is.EqualTo(60));
            Assert.That(building.Areas[Field.I], Is.EqualTo(75));
            Assert.That(building.Areas.ContainsKey(Field.J), Is.False);
        }

        [Test]
        public void GetReferenceHeightTest_Roof_Success()
        {
            var building = new FlatRoof(length: 10, width: 15, height: 10);

            Assert.That(building.GetReferenceHeight(), Is.EqualTo(6));
        }
    }
}
