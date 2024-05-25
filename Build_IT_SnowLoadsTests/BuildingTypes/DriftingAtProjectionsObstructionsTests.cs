using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class DriftingAtProjectionsObstructionsTests
    {
        [Test()]
        public void DriftingAtProjectionsObstructionsTest_Constructor_MinusValues()
        {
            var building = Mock.Of<IBuilding>();
            Assert.Throws<ArgumentOutOfRangeException>(()
                => new DriftingAtProjectionsObstructions(building, -20));
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_Constructor()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 2);

            Assert.AreEqual(2, driftingAtProjectionsObstructions.ObstructionHeight);
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_CalculateSnowLoad_ShapeCoefficientLessThan0Point8()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(1);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 2);

            driftingAtProjectionsObstructions.CalculateSnowLoad();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(0.8, driftingAtProjectionsObstructions.FirstShapeCoefficient);
                Assert.AreEqual(0.8, driftingAtProjectionsObstructions.SecondShapeCoefficient);
            });
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_CalculateSnowLoad_ShapeCoefficientLessThan2()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(1);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 5);

            driftingAtProjectionsObstructions.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0.8, driftingAtProjectionsObstructions.FirstShapeCoefficient);
                Assert.AreEqual(1, driftingAtProjectionsObstructions.SecondShapeCoefficient);
            });
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_CalculateSnowLoad_ShapeCoefficientMoreThan2()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(1);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 15);

            driftingAtProjectionsObstructions.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0.8, driftingAtProjectionsObstructions.FirstShapeCoefficient);
                Assert.AreEqual(2, driftingAtProjectionsObstructions.SecondShapeCoefficient);
            });
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_NotExceptional_CalculateSnowLoad()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(1);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 5);

            driftingAtProjectionsObstructions.CalculateSnowLoad();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(30, Math.Round(driftingAtProjectionsObstructions.SnowLoadOnRoofValue, 3));
                Assert.AreEqual(24, Math.Round(driftingAtProjectionsObstructions.SnowLoadOnRoofValueAtTheEnd, 3));
            });
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_Exceptional_CalculateSnowLoad()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.DesignExceptionalSnowLoadForSpecificReturnPeriod).Returns(10);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(1);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 5);

            driftingAtProjectionsObstructions.CalculateSnowLoad();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(60, Math.Round(driftingAtProjectionsObstructions.SnowLoadOnRoofValue, 3));
                Assert.AreEqual(48, Math.Round(driftingAtProjectionsObstructions.SnowLoadOnRoofValueAtTheEnd, 3));
            });
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_CalculateDriftLength_Between5And15()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 4);

            driftingAtProjectionsObstructions.CalculateDriftLength();
            Assert.AreEqual(8, Math.Round(driftingAtProjectionsObstructions.DriftLength, 3));
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_CalculateDriftLength_LessThan5()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 1);

            driftingAtProjectionsObstructions.CalculateDriftLength();
            Assert.AreEqual(5, Math.Round(driftingAtProjectionsObstructions.DriftLength, 3));
        }

        [Test()]
        public void DriftingAtProjectionsObstructionsTest_CalculateDriftLength_MoreThan15()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions(building.Object, 8);

            driftingAtProjectionsObstructions.CalculateDriftLength();
            Assert.AreEqual(15, Math.Round(driftingAtProjectionsObstructions.DriftLength, 3));
        }
    }
}