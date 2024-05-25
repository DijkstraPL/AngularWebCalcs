using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.Terrains.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.DynamicCharacteristicsTests
{
    [TestFixture]
    public class ReferenceHeightTests
    {
        [Test]
        [TestCase(1,2,2)]
        [TestCase(10,2,6)]
        public void GetFactorTest_Success(
            double height,
            double minimumHeight,
            double expectedResult)
        {
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(height);
            building.Setup(b => b.GetReferenceHeight()).Returns(height * 0.6);
            var terrain = new Mock<ITerrain>();
            terrain.Setup(t => t.MinimumHeight).Returns(minimumHeight);

            var referenceHeight = new ReferenceHeight(building.Object, terrain.Object);

            Assert.That(referenceHeight.GetFactor(), Is.EqualTo(expectedResult));
        }
    }
}
