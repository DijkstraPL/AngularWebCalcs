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
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.IntegrationTests
{
    [TestFixture]
    public class FlatRoofWithCurvedEavesWindLoadsTests
    {
        [Test]
        public void ExternalWindPressureForceCalculationsTest_Success()
        {
            //Arrange:
            double heightAboveSeaLevel = 250;
            double length = 30;
            double width = 30;
            double height = 10;
            double curvature = 2;
            WindZone windZone = WindZone.I;
            double referenceHeight = height;

            double actualLengthUpwindSlope = 70;
            double actualLengthDownwindSlope = 10;
            double effectiveFeatureHeight = 10;
            double horizontalDistanceFromCrestTop = -7;

            double windDirection = 0;

            var building = new FlatRoof(
                length, width, height, FlatRoof.Rotation.Degrees_0);
            var orographyFactor = new HillRidgeOrography(
                actualLengthUpwindSlope,
                actualLengthDownwindSlope,
                effectiveFeatureHeight,
                horizontalDistanceFromCrestTop);
            var terrain = new TerrainCategory0(orographyFactor);
            var directionalFactor = new DirectionalFactor(windZone, windDirection);
            var buildingSite = new BuildingSite(heightAboveSeaLevel, windZone, terrain,
                directionalFactor: directionalFactor);
            var referenceHeightDueToNeighbouringStructures =
                new ReferenceHeightDueToNeighbouringStructures(
                    highBuildingWidth: 20, highBuildingLength: 100,
                    highBuildingHeight: 100, distanceToBuilding: 30);
            var windLoadData = new WindLoadData(buildingSite, building, 
                referenceHeightDueToNeighbouringStructures: referenceHeightDueToNeighbouringStructures);
            var flatRoofWindLoads = new FlatRoofWithCurvedEavesWindLoads(
                building, windLoadData, curvature);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.SteelBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    flatRoofWindLoads,
                    structuralFactorCalculator);

            referenceHeight = windLoadData.GetReferenceHeightAt(referenceHeight);

            //Act:
            var resultMax = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                referenceHeight, calculateStructuralFactor: true);
            var resultMin = externalPressureWindForce.GetExternalPressureWindForceMinAt(
                referenceHeight, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                // e
                Assert.That(building.EdgeDistance, Is.EqualTo(20));
                // v_b,0
                Assert.That(buildingSite.FundamentalValueBasicWindVelocity, Is.EqualTo(22).Within(0.01));
                // c_dir
                Assert.That(directionalFactor.GetFactor(), Is.EqualTo(0.8));
                // v_b
                Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(17.6).Within(0.01));
                // z_e

                // c_r(z_e)
                Assert.That(terrain.GetRoughnessFactorAt(referenceHeight), Is.EqualTo(1.30).Within(0.01));
                // c_0(z_e)
                Assert.That(orographyFactor.GetFactorAt(referenceHeight), Is.EqualTo(1.17).Within(0.01));
                // v_m(z_e)
                Assert.That(windLoadData.GetMeanWindVelocityAt(referenceHeight),
                    Is.EqualTo(29.22).Within(0.01));
                // I_v(z_e)
                Assert.That(windLoadData.GetTurbulenceIntensityAt(referenceHeight),
                    Is.EqualTo(0.096).Within(0.001));
                // q_p(z_e)
                Assert.That(windLoadData.GetPeakVelocityPressureAt(referenceHeight),
                    Is.EqualTo(0.893).Within(0.001));
                // c_sc_d
                Assert.That(structuralFactorCalculator.GetStructuralFactor(true),
                    Is.EqualTo(1).Within(0.001));

                Assert.That(resultMax[Field.F], Is.EqualTo(-0.446).Within(0.001));
                Assert.That(resultMax[Field.G], Is.EqualTo(-0.446).Within(0.001));
                Assert.That(resultMax[Field.H], Is.EqualTo(-0.268).Within(0.001));
                Assert.That(resultMax[Field.I], Is.EqualTo(0.179).Within(0.001));

                Assert.That(resultMin[Field.F], Is.EqualTo(-0.446).Within(0.001));
                Assert.That(resultMin[Field.G], Is.EqualTo(-0.446).Within(0.001));
                Assert.That(resultMin[Field.H], Is.EqualTo(-0.268).Within(0.001));
                Assert.That(resultMin[Field.I], Is.EqualTo(-0.179).Within(0.001));
            });
        }
    }
}
