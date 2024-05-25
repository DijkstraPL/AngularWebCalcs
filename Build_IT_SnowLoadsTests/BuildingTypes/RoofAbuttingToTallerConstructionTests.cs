using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class RoofAbuttingToTallerConstructionTests
    {
        [Test()]
        public void RoofAbuttingToTallerConstructionTest_Constructor_MinusValues()
        {
            var building = new Mock<IBuilding>();
            var monopitchRoof = new Mock<IMonopitchRoof>();
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(()
                    => new RoofAbuttingToTallerConstruction(
                        building.Object, -20, 10, 5, monopitchRoof.Object));
                Assert.Throws<ArgumentOutOfRangeException>(()
                    => new RoofAbuttingToTallerConstruction(
                        building.Object, 20, 0, 5, monopitchRoof.Object));
                Assert.Throws<ArgumentOutOfRangeException>(()
                    => new RoofAbuttingToTallerConstruction(
                        building.Object, 20, 10, -5, monopitchRoof.Object));
            });
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_Constructor()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(30);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 5, roof.Object);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(20, roofAbuttingToTallerConstruction.WidthOfUpperBuilding);
                Assert.AreEqual(10, roofAbuttingToTallerConstruction.WidthOfLowerBuilding);
                Assert.AreEqual(5, roofAbuttingToTallerConstruction.HeightDifference);
                Assert.IsNotNull(roofAbuttingToTallerConstruction.UpperRoof);
                Assert.AreEqual(30, roofAbuttingToTallerConstruction.UpperRoof.Slope);
            });
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateDriftLength_Beteween5And15()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(30);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateDriftLength();

            Assert.AreEqual(10, roofAbuttingToTallerConstruction.DriftLength);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateDriftLength_LessThan5()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(30);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 1, roof.Object);

            roofAbuttingToTallerConstruction.CalculateDriftLength();

            Assert.AreEqual(5, roofAbuttingToTallerConstruction.DriftLength);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateDriftLength_MoreThan15()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(30);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 10, roof.Object);

            roofAbuttingToTallerConstruction.CalculateDriftLength();

            Assert.AreEqual(15, roofAbuttingToTallerConstruction.DriftLength);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_InvokeCalculateSnowLoadOnUpperRoof()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(30);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            roof.Verify(r => r.CalculateSnowLoad());
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_SlopeLargerThan15_ShapeCoefficientSlidingSnow()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(30);
            roof.Setup(r => r.SnowLoadOnRoofValue).Returns(3);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(1.5, roofAbuttingToTallerConstruction.ShapeCoefficientSlidingSnow);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_SlopeSmallerThan15_ShapeCoefficientSlidingSnow()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 10, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(0.8, roofAbuttingToTallerConstruction.ShapeCoefficientSlidingSnow);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_ShapeCoefficientWind_BetweenZeroPointEightAndFour()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(3);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(3.3333, roofAbuttingToTallerConstruction.ShapeCoefficientWind, 0.0001);
        }
        
        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_ShapeCoefficientWind_LessThanZeroPointEight()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(30);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(0.8, roofAbuttingToTallerConstruction.ShapeCoefficientWind, 0.0001);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_ShapeCoefficientWind_MoreThanFour()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(4, roofAbuttingToTallerConstruction.ShapeCoefficientWind, 0.0001);
        }
        
        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_ShapeCoefficient()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(4.8, roofAbuttingToTallerConstruction.ShapeCoefficient, 0.0001);
        }

        [Test()]
        public void CalculateSnowLoadTest_ShapeCoefficientAtTheEnd_DriftLengthSmallerThanLowerBuildingWidth()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(0.8, roofAbuttingToTallerConstruction.ShapeCoefficientAtTheEnd, 0.0001);
        }

        [Test()]
        public void CalculateSnowLoadTest_ShapeCoefficientAtTheEnd_DriftLengthLargerThanLowerBuildingWidth()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 2, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(2.56, roofAbuttingToTallerConstruction.ShapeCoefficientAtTheEnd, 0.0001);
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_NotExceptional()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            building.Setup(b => b.ThermalCoefficient).Returns(4);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(3);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(57.6, roofAbuttingToTallerConstruction.SnowLoadOnRoofValue, 0.0001);
                Assert.AreEqual(9.6, roofAbuttingToTallerConstruction.SnowLoadOnRoofValueAtTheEnd, 0.0001);
            });
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_Exceptional()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            building.Setup(b => b.ThermalCoefficient).Returns(4);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);
            snowLoad.Setup(sl => sl.DesignExceptionalSnowLoadForSpecificReturnPeriod).Returns(2);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(3);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(115.2, roofAbuttingToTallerConstruction.SnowLoadOnRoofValue, 0.0001);
                Assert.AreEqual(19.2, roofAbuttingToTallerConstruction.SnowLoadOnRoofValueAtTheEnd, 0.0001);
            });
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_NotExceptional_SnowLoadsNearTallerBuildingCases()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            building.Setup(b => b.ThermalCoefficient).Returns(4);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(3);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(9.6, roofAbuttingToTallerConstruction.SnowLoadsNearTallerBuilding[1], 0.0001);
                Assert.AreEqual(57.6, roofAbuttingToTallerConstruction.SnowLoadsNearTallerBuilding[2], 0.0001);
            });
        }

        [Test()]
        public void RoofAbuttingToTallerConstructionTest_CalculateSnowLoad_Exceptional_SnowLoadsNearTallerBuildingCases()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            building.Setup(b => b.ThermalCoefficient).Returns(4);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowDensity).Returns(2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(1);
            snowLoad.Setup(sl => sl.DesignExceptionalSnowLoadForSpecificReturnPeriod).Returns(2);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(3);

            var roof = new Mock<IMonopitchRoof>();
            roof.Setup(r => r.Slope).Returns(10);

            var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction(
                building.Object, widthOfUpperBuilding: 20,
                widthOfLowerBuilding: 20, heightDifference: 5, roof.Object);

            roofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(19.2, roofAbuttingToTallerConstruction.SnowLoadsNearTallerBuilding[1], 0.0001);
                Assert.AreEqual(115.2, roofAbuttingToTallerConstruction.SnowLoadsNearTallerBuilding[2], 0.0001);
            });
        }
    }
}