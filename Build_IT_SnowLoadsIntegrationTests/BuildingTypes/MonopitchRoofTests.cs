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
    public class MonopitchRoofTests
    {
        [Test()]
        [Description("Example number 2 from \"Obciążenia budynków i konstrukcji budowlanych według Eurokodów\" - Anna Rawska-Skotniczy")]
        public void ExampleTest2_CalculateSnowLoad_Success()
        {
            var buildingSite = new BuildingSite(Zones.ThirdZone, Topographies.Normal, 360);
            var snowLoad = new SnowLoad(buildingSite);
            var building = new Building(snowLoad);

            var monopitchRoof = new MonopitchRoof(building, 5);

            buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            monopitchRoof.CalculateSnowLoad();
            Assert.AreEqual(1.248, Math.Round(monopitchRoof.SnowLoadOnRoofValue, 3));
        }

        [Test()]
        [Description("Example number 4 from \"Obciążenia budynków i konstrukcji budowlanych według Eurokodów\" - Anna Rawska-Skotniczy")]
        public void ExampleTest4_CalculateSnowLoad_Success()
        {
            var buildingSite = new BuildingSite(Zones.SecondZone, Topographies.Normal, altitudeAboveSea: 175);
            var snowLoad = new SnowLoad(buildingSite);
            var building = new Building(snowLoad);
            var monopitchRoof = new MonopitchRoof(building, 10);

            buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            monopitchRoof.CalculateSnowLoad();
            Assert.AreEqual(0.72, Math.Round(monopitchRoof.SnowLoadOnRoofValue, 3));
        }
    }
}
