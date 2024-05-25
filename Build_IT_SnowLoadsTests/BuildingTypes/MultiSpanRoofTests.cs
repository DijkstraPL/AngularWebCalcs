using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class MultiSpanRoofTests
    {
        [Test()]
        public void MultiSpanRoofTest_Constructor()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var multiSpanRoof = new MultiSpanRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(45, multiSpanRoof.LeftRoof.Slope);
                Assert.AreEqual(15, multiSpanRoof.RightRoof.Slope);
            });
        }

        [Test()]
        public void MultiSpanRoofTest_CalculateSnowLoad_ShapeCoefficientCalculated()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(0);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var multiSpanRoof = new MultiSpanRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            multiSpanRoof.CalculateSnowLoad();
            Assert.That(multiSpanRoof.ShapeCoefficient, Is.EqualTo(1.2).Within(0.0001));
        }

        [Test()]
        public void MultiSpanRoofTest_CalculateSnowLoad_ShapeCoefficientSetToOnePointSix()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(70);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var multiSpanRoof = new MultiSpanRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            multiSpanRoof.CalculateSnowLoad();
            Assert.That(multiSpanRoof.ShapeCoefficient, Is.EqualTo(1.6));
        }

        [Test()]
        public void MultiSpanRoofTest_CalculateSnowLoad_InvokeCalculationOnRoofSides()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var multiSpanRoof = new MultiSpanRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            multiSpanRoof.CalculateSnowLoad();
            Assert.Multiple(() =>
            {
                leftRoof.Verify(lr => lr.CalculateSnowLoad());
                rightRoof.Verify(lr => lr.CalculateSnowLoad());
            });
        }

        [Test()]
        public void MultiSpanRoofTest_CalculateSnowLoad_NotExcepctional()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var multiSpanRoof = new MultiSpanRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            multiSpanRoof.CalculateSnowLoad();

            Assert.AreEqual(48, multiSpanRoof.SnowLoadOnMiddleRoof, 0.0001);
        }


        [Test()]
        public void MultiSpanRoofTest_CalculateSnowLoad_Excepctional()
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

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var multiSpanRoof = new MultiSpanRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            multiSpanRoof.CalculateSnowLoad();

            Assert.AreEqual(96, multiSpanRoof.SnowLoadOnMiddleRoof, 0.0001);
        }
    }
}