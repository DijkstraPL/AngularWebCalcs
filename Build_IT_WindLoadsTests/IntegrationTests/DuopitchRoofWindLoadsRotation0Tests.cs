using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.Factors;
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
    public class DuopitchRoofWindLoadsRotation0Tests
    {
        [Test]
        public void ExternalWindPressureForceCalculationsTest_Success()
        {
            //Arrange:
            double heightAboveSeaLevel = 250;
            double length = 30;
            double width = 30;
            double outerHeight = 17.355;
            double middleHeight = 20;
            WindZone windZone = WindZone.I;
            double referenceHeight = outerHeight;

            var building = new DuopitchRoof(
                length, width, outerHeight, middleHeight, DuopitchRoof.Rotation.Degrees_0);
            var heightDisplacement = new HeightDisplacement(
                building, horizontalDistanceToObstruction: 10, obstructionHeight: 15);
            var terrain = new TerrainCategoryIV(heightDisplacement);
            var buildingSite = new BuildingSite(heightAboveSeaLevel, windZone, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var flatRoofWindLoads = new DuopitchRoofWindLoadsRotation0(
                building, windLoadData);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    flatRoofWindLoads);

            referenceHeight = windLoadData.GetReferenceHeightAt(referenceHeight)
                - heightDisplacement.GetFactor();

            //Act:
            var resultMax = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                building.Height, calculateStructuralFactor: true);
            var resultMin = externalPressureWindForce.GetExternalPressureWindForceMinAt(
                building.Height, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                // e
                Assert.That(building.EdgeDistance, Is.EqualTo(30));
                // v_b,0
                Assert.That(buildingSite.FundamentalValueBasicWindVelocity, Is.EqualTo(22).Within(0.01));
                // c_dir

                // v_b
                Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(22).Within(0.01));
                // z_e

                // c_r(z_e)
                Assert.That(terrain.GetRoughnessFactorAt(referenceHeight), Is.EqualTo(0.6).Within(0.01));
                // c_0(z_e)

                // v_m(z_e)
                Assert.That(windLoadData.GetMeanWindVelocityAt(referenceHeight),
                    Is.EqualTo(13.2).Within(0.01));
                // I_v(z_e)
                Assert.That(windLoadData.GetTurbulenceIntensityAt(referenceHeight),
                    Is.EqualTo(0.434).Within(0.001));
                // q_p(z_e)
                Assert.That(windLoadData.GetPeakVelocityPressureAt(referenceHeight),
                    Is.EqualTo(0.440).Within(0.001));
                // c_sc_d

                Assert.That(resultMax[Field.F], Is.EqualTo(0.044).Within(0.001));
                Assert.That(resultMax[Field.G], Is.EqualTo(0.044).Within(0.001));
                Assert.That(resultMax[Field.H], Is.EqualTo(0.044).Within(0.001));
                Assert.That(resultMax[Field.I], Is.EqualTo(-0.132).Within(0.001));
                Assert.That(resultMax[Field.J], Is.EqualTo(0.044).Within(0.001));

                Assert.That(resultMin[Field.F], Is.EqualTo(-0.572).Within(0.001));
                Assert.That(resultMin[Field.G], Is.EqualTo(-0.440).Within(0.001));
                Assert.That(resultMin[Field.H], Is.EqualTo(-0.198).Within(0.001));
                Assert.That(resultMin[Field.I], Is.EqualTo(-0.220).Within(0.001));
                Assert.That(resultMin[Field.J], Is.EqualTo(-0.352).Within(0.001));
            });
        }
    }
}
