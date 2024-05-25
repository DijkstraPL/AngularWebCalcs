using Build_IT_SnowLoads;
using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_SnowLoadsIntegrationTests.BuildingTypes
{
    [TestFixture]
    public class PitchedRoofTests
    {
        [Test()]
        [Description("Example number 1 from \"Obciążenia budynków i konstrukcji budowlanych według Eurokodów\" - Anna Rawska-Skotniczy")]
        public void ExampleTest1_CalculateSnowLoad_Success()
        {
            var buildingSite = new BuildingSite(Zones.FirstZone, Topographies.Normal, 127);
            buildingSite.CalculateExposureCoefficient();
            var snowLoad = new SnowLoad(buildingSite);
            snowLoad.CalculateSnowLoad();
            var building = new Building(snowLoad);
            building.CalculateThermalCoefficient();

            var pitchedRoof = new PitchedRoof(building, 35, 5);

            pitchedRoof.CalculateSnowLoad();
            Assert.AreEqual(0.467, pitchedRoof.LeftRoof.SnowLoadOnRoofValue, 0.001);
            Assert.AreEqual(0.56, pitchedRoof.RightRoof.SnowLoadOnRoofValue, 0.001);
        }
    }
}
