using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.DynamicCharacteristics.Enums;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.TerrainOrographies;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.WindLoadsCases;
using Build_IT_WindLoads.WindLoadsCases.Roofs;
using NUnit.Framework;
using System;
using System.Linq;

namespace Build_IT_WindLoadsTests.IntegrationTests
{
    [TestFixture]
    public class HippedRoofWindLoadsTests
    {
        [Test]
        public void ExternalWindPressureCalculationsTest_Roof1_MaxValues_Rotation0_Success()
        {
            //Arrange:
            var windZone = WindZone.II;
            var outerHeightDifference = Math.Tan(10 * Math.PI / 180) * 15;
            var ridgeLength = 30 - Math.Tan(10 * Math.PI / 180) * 15 / Math.Tan(25 * Math.PI / 180) * 2;
            var building = new HippedRoof(length: 30, width: 30, 
                middleHeight: 20, outerHeight: 20 - outerHeightDifference, 
                ridgeLength: ridgeLength, rotation: HippedRoof.Rotation.Degrees_0);
            var terrain = new TerrainCategoryII();
            var buildingSite = new BuildingSite(
                220, windZone, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var hippedRoofWindLoads = new HippedRoofWindLoads(building, windLoadData);

            //Act:
            var result = hippedRoofWindLoads.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(0.116).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(0.116).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(0.116).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(-0.464).Within(0.001));
                Assert.That(result[Field.J], Is.EqualTo(-0.928).Within(0.001));
                Assert.That(result[Field.K], Is.EqualTo(-1.044).Within(0.001));
                Assert.That(result[Field.L], Is.EqualTo(-1.508).Within(0.001));
                Assert.That(result[Field.M], Is.EqualTo(-0.696).Within(0.001));
            });
        }
        [Test]
        public void ExternalWindPressureCalculationsTest_Roof1_MaxValues_Rotation90_Success()
        {
            //Arrange:
            var windZone = WindZone.II;
            var outerHeightDifference = Math.Tan(10 * Math.PI / 180) * 15;
            var ridgeLength = 30 - Math.Tan(10 * Math.PI / 180) * 15 / Math.Tan(25 * Math.PI / 180) * 2;
            var building = new HippedRoof(length: 30, width: 30,
                middleHeight: 20, outerHeight: 20 - outerHeightDifference,
                ridgeLength: ridgeLength, rotation: HippedRoof.Rotation.Degrees_90);
            var terrain = new TerrainCategoryII();
            var buildingSite = new BuildingSite(
                220, windZone, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var hippedRoofWindLoads = new HippedRoofWindLoads(building, windLoadData);

            //Act:
            var result = hippedRoofWindLoads.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(0.464).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(0.618).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(0.386).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(-0.502).Within(0.001));
                Assert.That(result[Field.J], Is.EqualTo(-0.928).Within(0.001));
                Assert.That(result[Field.L], Is.EqualTo(-1.624).Within(0.001));
                Assert.That(result[Field.M], Is.EqualTo(-0.850).Within(0.001));
                Assert.That(result[Field.N], Is.EqualTo(-0.270).Within(0.001));
            });
        }
    }
}
