using Build_IT_SnowLoads;
using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Enums;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class SnowOverhangingTests
    {
        [Test()]
        public void SnowOverhangingTest_Constructor_MinusValues_Success()
        {
            var building = new Mock<IBuilding>();

            Assert.Throws<ArgumentOutOfRangeException>(()
                   => new SnowOverhanging(building.Object, -0.2, 0.72));
        }

        [Test()]
        public void SnowOverhangingTest_Constructor_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);

            var snowOverhanging = new SnowOverhanging(building.Object, snowLayerDepth: 0.2, snowLoadOnRoof: 0.72);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0.2, snowOverhanging.SnowLayerDepth);
                Assert.AreEqual(0.72, snowOverhanging.SnowLoadOnRoofValue);
            });
        }

        [Test()]
        public void SnowOverhangingTest_CalculateSnowLoad_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);

            var snowOverhanging = new SnowOverhanging(building.Object, snowLayerDepth: 0.2, snowLoadOnRoof: 0.72);

            snowOverhanging.CalculateSnowLoad();
            Assert.AreEqual(0.104, snowOverhanging.SnowLoad,0.001);
        }
    }
}