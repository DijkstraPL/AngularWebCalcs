using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Enums;
using Build_IT_SnowLoads.Exceptional;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.Exceptional
{
    [TestFixture()]
    public class ExceptionalRoofAbuttingToTallerConstructionTests
    {
        [Test()]
        [Description("Check constructor for the ExceptionalRoofAbuttingToTallerConstruction.")]
        public void ExceptionalRoofAbuttingToTallerConstructionTest_Constructor_Success()
        {
            var building = BuildingImplementation.CreateBuilding();

            var exceptionalRoofAbuttingToTallerConstruction = 
                new ExceptionalRoofAbuttingToTallerConstruction(building, 20, 10, 3, 15);

            Assert.IsNotNull(exceptionalRoofAbuttingToTallerConstruction, 
                "ExceptionalRoofAbuttingToTallerConstruction should be created.");
            Assert.AreEqual(20, exceptionalRoofAbuttingToTallerConstruction.UpperBuildingWidth, 
                "Width should be set at construction time.");
            Assert.AreEqual(10, exceptionalRoofAbuttingToTallerConstruction.LowerBuildingWidth,
                "Width should be set at construction time.");
            Assert.AreEqual(3, exceptionalRoofAbuttingToTallerConstruction.HeightDifference,
                "Height should be set at construction time.");
            Assert.AreEqual(15, exceptionalRoofAbuttingToTallerConstruction.Angle,
                "Angle should be set at construction time.");
        }

        [Test()]
        [Description("Check calculations of snow loads for the ExceptionalRoofAbuttingToTallerConstruction.")]
        public void ExceptionalRoofAbuttingToTallerConstructionTest_CalculateSnowLoad_Success()
        {
            var building = BuildingImplementation.CreateBuilding();
            building.SnowLoadImplementation.ExcepctionalSituation = true;
            building.SnowLoadImplementation.CurrentDesignSituation = DesignSituation.B2;

            var exceptionalRoofAbuttingToTallerConstruction =
                new ExceptionalRoofAbuttingToTallerConstruction(building, 20, 10, 3, 15);

            exceptionalRoofAbuttingToTallerConstruction.CalculateSnowLoad();

            Assert.AreEqual(3.6, Math.Round(exceptionalRoofAbuttingToTallerConstruction.SnowLoadNearTheTop, 3),
                "Snow load for roof is not calculated properly.");
            Assert.AreEqual(3.6, Math.Round(exceptionalRoofAbuttingToTallerConstruction.SnowLoadNearTheEdge, 3),
                "Snow load for roof is not calculated properly.");
        }

        [Test()]
        [Description("Check calculations of drift length for the ExceptionalRoofAbuttingToTallerConstruction.")]
        public void ExceptionalRoofAbuttingToTallerConstructionTest_CalculateDriftLength_Success()
        {
            var building = BuildingImplementation.CreateBuilding();

            var exceptionalRoofAbuttingToTallerConstruction =
                new ExceptionalRoofAbuttingToTallerConstruction(building, 20, 10, 3, 15);

            exceptionalRoofAbuttingToTallerConstruction.CalculateDriftLength();
            Assert.AreEqual(10, Math.Round(exceptionalRoofAbuttingToTallerConstruction.DriftLength, 3),
                "Drift length for roof is not calculated properly.");
        }
        
        [Test()]
        [Description("Example number 3 from \"Obciążenia budynków i konstrukcji budowlanych według Eurokodów\" - Anna Rawska-Skotniczy")]
        public void ExampleTest3_CalculateSnowLoad_Success()
        {
            var buildingSite = new BuildingSite(Zones.FirstZone, Topographies.Normal, 127);
            buildingSite.CalculateExposureCoefficient();
            var snowLoad = new SnowLoad(buildingSite, DesignSituation.B2, true);
            snowLoad.CalculateSnowLoad();
            var building = new Building(snowLoad);
            building.CalculateThermalCoefficient();

            var roofAbuttingToTallerConstruction5Degrees = new ExceptionalRoofAbuttingToTallerConstruction(building,12,14.6,4.5, 5);
            roofAbuttingToTallerConstruction5Degrees.CalculateDriftLength();
            roofAbuttingToTallerConstruction5Degrees.CalculateSnowLoad();

            var roofAbuttingToTallerConstruction35Degrees = new ExceptionalRoofAbuttingToTallerConstruction(building, 12, 14.6, 4.5, 35);
            roofAbuttingToTallerConstruction35Degrees.CalculateDriftLength();
            roofAbuttingToTallerConstruction35Degrees.CalculateSnowLoad();

            Assert.AreEqual(14.6, Math.Round(roofAbuttingToTallerConstruction5Degrees.DriftLength, 3),
                "Drift length is not calculated properly.");
            Assert.AreEqual(1.4, Math.Round(roofAbuttingToTallerConstruction5Degrees.SnowLoadNearTheTop, 3),
                "Snow load for roof is not calculated properly.");
            Assert.AreEqual(1.4, Math.Round(roofAbuttingToTallerConstruction5Degrees.SnowLoadNearTheEdge, 3),
                "Snow load for roof is not calculated properly.");
            Assert.AreEqual(0, Math.Round(roofAbuttingToTallerConstruction35Degrees.SnowLoadNearTheTop, 3),
                "Snow load for roof is not calculated properly.");
            Assert.AreEqual(1.167, Math.Round(roofAbuttingToTallerConstruction35Degrees.SnowLoadNearTheEdge, 3),
                "Snow load for roof is not calculated properly.");
        }
    }
}