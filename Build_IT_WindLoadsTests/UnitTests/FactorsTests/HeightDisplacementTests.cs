using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.Terrains.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.FactorsTests
{
    [TestFixture]
    public class HeightDisplacementTests
    {
        [Test]
        public void Constructor_Success()
        {
            var building = new Mock<IStructure>();
            var heightDisplacement = new HeightDisplacement(
                building.Object,
                horizontalDistanceToObstruction: 10, 
                obstructionHeight: 12);

            Assert.That(heightDisplacement.HorizontalDistanceToObstruction, Is.EqualTo(10));
            Assert.That(heightDisplacement.ObstructionsHeight, Is.EqualTo(12));
        }

        [Test]
        [TestCase(10, 12, 6, Description ="x <= 2 * h_ave -> 0.6 * h")]
        [TestCase(9, 5, 4, Description = "x <= 2 * h_ave -> 0.8 * h_ave")]
        [TestCase(11, 5, 3.8, Description = "2 * h_ave < x < 6 * h_ave -> (1.2 * h_ave - 0.2 * x)")]
        [TestCase(61, 30, 6, Description = "2 * h_ave < x < 6 * h_ave -> 0.6 * h")]
        [TestCase(10, 1, 0, Description = "x >= 6 * h_ave -> 0")]
        public void GetFactor_Success(
            double horizontalDistanceToObstruction,
            double obstructionHeight,
            double expectedResult)
        {
            var terrain = new Mock<ITerrain>();
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(10);
            var heightDisplacement = new HeightDisplacement(
                building.Object,
                horizontalDistanceToObstruction,
                obstructionHeight);

            Assert.That(heightDisplacement.GetFactor(), Is.EqualTo(expectedResult));
        }
    }
}
