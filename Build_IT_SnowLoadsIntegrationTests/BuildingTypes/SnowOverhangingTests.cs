using Build_IT_SnowLoads;
using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Enums;
using NUnit.Framework;

namespace Build_IT_SnowLoadsIntegrationTests.BuildingTypes
{
    [TestFixture]
   public  class SnowOverhangingTests
    {
        [Test()]
        [Description("Example number 2 from \"Obciążenia budynków i konstrukcji budowlanych według Eurokodów\" - Anna Rawska-Skotniczy")]
        public void ExampleTest2_CalculateSnowOverhanging_Success()
        {
            var buildingSite = new BuildingSite(Zones.ThirdZone, Topographies.Normal, 360);
            buildingSite.CalculateExposureCoefficient();
            var snowLoad = new SnowLoad(buildingSite, snowDensity: 3);
            snowLoad.CalculateSnowLoad();
            var building = new Building(snowLoad);
            building.CalculateThermalCoefficient();

            var monopitchRoof = new MonopitchRoof(building, 5);
            monopitchRoof.CalculateSnowLoad();

            var snowOverhanging = new SnowOverhanging(building, monopitchRoof.SnowLoadOnRoofValue);
            snowOverhanging.CalculateSnowLoad();

            Assert.AreEqual(0.648, snowOverhanging.SnowLoad,0.001);
        }
    }
}
