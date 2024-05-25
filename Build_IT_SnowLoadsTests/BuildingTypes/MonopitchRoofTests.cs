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
    public class MonopitchRoofTests
    {
        [Test()]
        public void MonopitchRoofTest_Constructor_MinusValues_Success()
        {
            var building = new Mock<IBuilding>();

            Assert.Throws<ArgumentOutOfRangeException>(()
                => new MonopitchRoof(building.Object, -20));
        }

        [Test()]
        public void MonopitchRoofTest_Constructor_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var monopitchRoof = new MonopitchRoof(building.Object, 15);

            Assert.AreEqual(15, monopitchRoof.Slope);
        }

        [Test()]
        public void MonopitchRoofTest_CalculateSnowLoad_NotExceptional_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var monopitchRoof = new MonopitchRoof(building.Object, 15);

            monopitchRoof.CalculateSnowLoad();
            Assert.AreEqual(24, monopitchRoof.SnowLoadOnRoofValue, 0.00001);
        }

        [Test()]
        public void MonopitchRoofTest_CalculateSnowLoad_Exceptional_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.DesignExceptionalSnowLoadForSpecificReturnPeriod).Returns(10);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var monopitchRoof = new MonopitchRoof(building.Object, 15);

            monopitchRoof.CalculateSnowLoad();
            Assert.AreEqual(48, monopitchRoof.SnowLoadOnRoofValue, 0.00001);
        }


    }
}