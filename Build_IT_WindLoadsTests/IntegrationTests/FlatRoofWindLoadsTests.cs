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
using System.Linq;

namespace Build_IT_WindLoadsTests.IntegrationTests
{
    [TestFixture]
    public class FlatRoofWindLoadsTests
    {
        [Test]
        public void ExternalWindPressureCalculationsTest_Roof1_MaxValues_Success()
        {
            //Arrange:
            var windZone = WindZone.III;
            var building = new FlatRoof(20, 10, 15);
            var terrainOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 15,
                effectiveFeatureHeight: 5,
                horizontalDistanceFromCrestTop: 2);
            var terrain = new TerrainCategoryIII(terrainOrography);
            var directionalFactor = new DirectionalFactor(windZone, windDirection: 40);
            var buildingSite = new BuildingSite(
                325, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new FlatRoofWindLoads(building, windLoadData);

            //Act:
            var result = flatRoofWindLoads.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(-0.818).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(-0.530).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(-0.258).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(0.074).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureCalculationsTest_Roof1_MinValues_Success()
        {
            //Arrange:
            var windZone = WindZone.III;
            var building = new FlatRoof(20, 10, 15);
            var terrainOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 15,
                effectiveFeatureHeight: 5,
                horizontalDistanceFromCrestTop: 2);
            var terrain = new TerrainCategoryIII(terrainOrography);
            var directionalFactor = new DirectionalFactor(windZone, windDirection: 40);
            var buildingSite = new BuildingSite(
                325, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new FlatRoofWindLoads(building, windLoadData);

            //Act:
            var result = flatRoofWindLoads.GetExternalWindPressureMinAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(-0.818).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(-0.530).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(-0.258).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(-0.074).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureCalculationsTest_Roof2_MaxValues_Success()
        {
            //Arrange:
            var windZone = WindZone.III;
            var building = new FlatRoof(20, 10, 15);
            var terrainOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 15,
                effectiveFeatureHeight: 5,
                horizontalDistanceFromCrestTop: 2);
            var terrain = new TerrainCategoryIII(terrainOrography);
            var directionalFactor = new DirectionalFactor(windZone, windDirection: 40);
            var buildingSite = new BuildingSite(
                325, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new FlatRoofWindLoads(building, windLoadData);

            //Act:
            var result = flatRoofWindLoads.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(-0.818).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(-0.530).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(-0.258).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(0.073).Within(0.001));
            });
        }
        [Test]
        public void ExternalWindPressureCalculationsTest_Roof2_MinValues_Success()
        {
            //Arrange:
            var windZone = WindZone.III;
            var building = new FlatRoof(20, 10, 15);
            var terrainOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 15,
                effectiveFeatureHeight: 5,
                horizontalDistanceFromCrestTop: 2);
            var terrain = new TerrainCategoryIII(terrainOrography);
            var directionalFactor = new DirectionalFactor(windZone, windDirection: 40);
            var buildingSite = new BuildingSite(
                325, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new FlatRoofWindLoads(building, windLoadData);

            //Act:
            var result = flatRoofWindLoads.GetExternalWindPressureMinAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(-0.818).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(-0.530).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(-0.258).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(-0.073).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureCalculationsTest_Roof3_MaxValues_Success()
        {
            //Arrange:
            var windZone = WindZone.III;
            var building = new FlatRoof(20, 10, 15);
            var terrainOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 15,
                effectiveFeatureHeight: 5,
                horizontalDistanceFromCrestTop: 2);
            var terrain = new TerrainCategoryIII(terrainOrography);
            var directionalFactor = new DirectionalFactor(windZone, windDirection: 40);
            var buildingSite = new BuildingSite(
                325, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new FlatRoofWindLoads(building, windLoadData);

            //Act:
            var result = flatRoofWindLoads.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.F], Is.EqualTo(-0.818).Within(0.001));
                Assert.That(result[Field.G], Is.EqualTo(-0.530).Within(0.001));
                Assert.That(result[Field.H], Is.EqualTo(-0.258).Within(0.001));
                Assert.That(result[Field.I], Is.EqualTo(0.073).Within(0.001));
            });
        }
     
        [Test]
        public void ExternalWindPressureForceCalculationsTest_Example2019_04_28_Success()
        {
            //Arrange:
            double heightAboveSeaLevel = 123;
            double length = 40;
            double width = 50;
            double height = 30;
            WindZone windZone = WindZone.II;
            double referenceHeight = 30;

            double actualLengthUpwindSlope = 20;
            double actualLengthDownwindSlope = 10;
            double effectiveFeatureHeight = 10;
            double horizontalDistanceFromCrestTop = 2;

            double windDirection = 220;

            var building = new FlatRoof(
                length, width, height);
            var orographyFactor = new HillRidgeOrography(
                actualLengthUpwindSlope,
                actualLengthDownwindSlope,
                effectiveFeatureHeight,
                horizontalDistanceFromCrestTop);
            var terrain = new TerrainCategoryIII(orographyFactor);
            var directionalFactor = new DirectionalFactor(windZone, windDirection);
            var buildingSite = new BuildingSite(heightAboveSeaLevel, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new FlatRoofWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.ReinforcementConcreteBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    flatRoofWindLoads,
                    structuralFactorCalculator);

            //Act:
            var resultMax = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                referenceHeight, calculateStructuralFactor: true);
            var resultMin = externalPressureWindForce.GetExternalPressureWindForceMinAt(
                referenceHeight, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                // e
                Assert.That(building.EdgeDistance, Is.EqualTo(50));
                // v_b,0
                Assert.That(buildingSite.FundamentalValueBasicWindVelocity, Is.EqualTo(26).Within(0.01));
                // c_dir
                Assert.That(directionalFactor.GetFactor(), Is.EqualTo(0.8));
                // v_b
                Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(20.8).Within(0.01));
                // z_e

                // c_r(z_e)
                Assert.That(terrain.GetRoughnessFactorAt(referenceHeight), Is.EqualTo(0.99).Within(0.01));
                // c_0(z_e)
                Assert.That(orographyFactor.GetFactorAt(referenceHeight), Is.EqualTo(1.11).Within(0.01));
                // v_m(z_e)
                Assert.That(windLoadData.GetMeanWindVelocityAt(referenceHeight),
                    Is.EqualTo(22.85).Within(0.01));
                // I_v(z_e)
                Assert.That(windLoadData.GetTurbulenceIntensityAt(referenceHeight),
                    Is.EqualTo(0.195).Within(0.001));
                // q_p(z_e)
                Assert.That(windLoadData.GetPeakVelocityPressureAt(referenceHeight),
                    Is.EqualTo(0.771).Within(0.001));
                // c_sc_d
                Assert.That(structuralFactorCalculator.GetStructuralFactor(true),
                    Is.EqualTo(0.817).Within(0.001));

                Assert.That(resultMax[Field.F], Is.EqualTo(-1.134).Within(0.001));
                Assert.That(resultMax[Field.G], Is.EqualTo(-0.756).Within(0.001));
                Assert.That(resultMax[Field.H], Is.EqualTo(-0.441).Within(0.001));
                Assert.That(resultMax[Field.I], Is.EqualTo(0.126).Within(0.001));

                Assert.That(resultMin[Field.F], Is.EqualTo(-1.134).Within(0.001));
                Assert.That(resultMin[Field.G], Is.EqualTo(-0.756).Within(0.001));
                Assert.That(resultMin[Field.H], Is.EqualTo(-0.441).Within(0.001));
                Assert.That(resultMin[Field.I], Is.EqualTo(-0.126).Within(0.001));
            });
        }
    }
}
