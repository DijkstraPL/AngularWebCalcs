using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Walls;
using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.DynamicCharacteristics.Enums;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.TerrainOrographies;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.WindLoadsCases;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.IntegrationTests
{
    [TestFixture]
    public class WallsWindLoadsTests
    {
        [Test]
        public void GetPeakVelocityPressureTest_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(10, 10, 10);
            var terrain = new TerrainCategoryI();

            var buildingSite = new BuildingSite(325, WindZone.I, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);

            //Act:
            var result = windLoadData.GetPeakVelocityPressureAt(10);

            //Assert:
            Assert.That(result, Is.EqualTo(0.904).Within(0.001));
        }

        [Test]
        public void ExternalWindPressureCalculationsTest_Walls_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(10, 10, 15);
            var terrain = new TerrainCategoryIII();
            var buildingSite = new BuildingSite(325, WindZone.I_III, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            //Act:
            var result = verticalWallOfRectangularBuilding.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.754).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.503).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.503).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.330).Within(0.001));
            });
        }
        
        [Test]
        public void ExternalWindPressureCalculationsTest_AS_SX01a_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 72, width: 30, height: 7.3);
            var terrain = new TerrainCategoryII();
            var buildingSite = new BuildingSite(325, WindZone.II, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            //Act:
            var result = verticalWallOfRectangularBuilding.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.D], Is.EqualTo(0.638).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.273).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureCalculationsTest_8_1a_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 28.8, width: 42, height: 10.6);
            var terrain = new TerrainCategoryIII();
            var buildingSite = new BuildingSite(175, WindZone.I, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            //Act:
            var result = verticalWallOfRectangularBuilding.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.704).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.470).Within(0.001));
                Assert.That(result[Field.C], Is.EqualTo(-0.294).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.420).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.194).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureCalculationsTest_8_1b_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 28.8, width: 42, height: 10.6, 
                EqualHeightWalls.Rotation.Degrees_90);
            var terrain = new TerrainCategoryIII();
            var buildingSite = new BuildingSite(175, WindZone.I, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            //Act:
            var result = verticalWallOfRectangularBuilding.GetExternalWindPressureMaxAt(building.Height);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.704).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.470).Within(0.001));
                Assert.That(result[Field.C], Is.EqualTo(-0.294).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.411).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.177).Within(0.001));
            });
        }
        
        [Test]
        public void ExternalWindPressureForceCalculationsTest_8_2_48m_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 16, width: 20, height: 48);
            var heightDisplacement = new HeightDisplacement(
                building,
                horizontalDistanceToObstruction: 10,
                obstructionHeight: 15);
            var terrain = new TerrainCategoryIV(heightDisplacement);
            var buildingSite = new BuildingSite(360, WindZone.III, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.SteelBuilding);

            var externalPressureWindForce = 
                new ExternalPressureWindForce(
                    windLoadData, 
                    verticalWallOfRectangularBuilding, 
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                building.Height, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.628).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.418).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.418).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.314).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureForceCalculationsTest_8_2_28m_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 16, width: 20, height: 48);
            var heightDisplacement = new HeightDisplacement(
                building,
                horizontalDistanceToObstruction: 10,
                obstructionHeight: 15);
            var terrain = new TerrainCategoryIV(heightDisplacement);
            var buildingSite = new BuildingSite(360, WindZone.III, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.SteelBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    verticalWallOfRectangularBuilding,
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                28, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.507).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.338).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.338).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.254).Within(0.001));
            });
        }
        
        [Test]
        public void ExternalWindPressureForceCalculationsTest_8_2_24m_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 16, width: 20, height: 48);
            var heightDisplacement = new HeightDisplacement(
                building,
                horizontalDistanceToObstruction: 10,
                obstructionHeight: 15);
            var terrain = new TerrainCategoryIV(heightDisplacement);
            var buildingSite = new BuildingSite(360, WindZone.III, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.SteelBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    verticalWallOfRectangularBuilding,
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                24, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.479).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.319).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.319).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.239).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureForceCalculationsTest_8_2_20m_Success()
        {
            //Arrange:
            var building = new EqualHeightWalls(
                length: 16, width: 20, height: 48);
            var heightDisplacement = new HeightDisplacement(
                building,
                horizontalDistanceToObstruction: 10,
                obstructionHeight: 15);
            var terrain = new TerrainCategoryIV(heightDisplacement);
            var buildingSite = new BuildingSite(360, WindZone.III, terrain);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.SteelBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    verticalWallOfRectangularBuilding,
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                20, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                Assert.That(result[Field.A], Is.EqualTo(-0.464).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.309).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.309).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.232).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureForceCalculationsTest_Example2019_04_27_Success()
        {
            //Arrange:
            double heightAboveSeaLevel = 400;
            double length = 110;
            double width = 70;
            double height = 200;
            WindZone windZone = WindZone.III;
            double referenceHeight = 200;

            double actualLengthUpwindSlope = 20;
            double actualLengthDownwindSlope = 10;
            double effectiveFeatureHeight = 10;
            double horizontalDistanceFromCrestTop = -2;

            double windDirection = 20;
            
            var building = new EqualHeightWalls(
                length, width, height);
            var orographyFactor = new HillRidgeOrography(
                actualLengthUpwindSlope,
                actualLengthDownwindSlope,
                effectiveFeatureHeight,
                horizontalDistanceFromCrestTop);
            var terrain = new TerrainCategoryI( orographyFactor);
            var directionalFactor = new DirectionalFactor(windZone, windDirection);
            var buildingSite = new BuildingSite(heightAboveSeaLevel, windZone, terrain, 
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.ReinforcementConcreteBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    verticalWallOfRectangularBuilding,
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
               referenceHeight, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                // e
                Assert.That(building.EdgeDistance, Is.EqualTo(70));
                // v_b,0
                Assert.That(buildingSite.FundamentalValueBasicWindVelocity, Is.EqualTo(23.32).Within(0.01));
                // c_dir
                Assert.That(directionalFactor.GetFactor(), Is.EqualTo(0.7));
                // v_b
                Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(16.32).Within(0.01));
                // z_e

                // c_r(z_e)
                Assert.That(terrain.GetRoughnessFactorAt(referenceHeight), Is.EqualTo(1.77).Within(0.01));
                // c_0(z_e)
                Assert.That(orographyFactor.GetFactorAt(referenceHeight), Is.EqualTo(1));
                // v_m(z_e)
                Assert.That(windLoadData.GetMeanWindVelocityAt(referenceHeight), 
                    Is.EqualTo(28.92).Within(0.01));
                // I_v(z_e)
                Assert.That(windLoadData.GetTurbulenceIntensityAt(referenceHeight),
                    Is.EqualTo(0.101).Within(0.001));
                // q_p(z_e)
                Assert.That(windLoadData.GetPeakVelocityPressureAt(referenceHeight),
                    Is.EqualTo(0.857).Within(0.001));
                // c_sc_d
                Assert.That(structuralFactorCalculator.GetStructuralFactor(true),
                    Is.EqualTo(0.878).Within(0.001));

                Assert.That(result[Field.A], Is.EqualTo(-0.903).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.602).Within(0.001));
                Assert.That(result[Field.C], Is.EqualTo(-0.376).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.602).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.407).Within(0.001));
            });
        }

        [Test]
        public void ExternalWindPressureForceCalculationsTest_Example2019_04_27_At100meters_Success()
        {
            //Arrange:
            double heightAboveSeaLevel = 400;
            double length = 110;
            double width = 70;
            double height = 200;
            WindZone windZone = WindZone.III;
            double referenceHeight = 100;

            double actualLengthUpwindSlope = 20;
            double actualLengthDownwindSlope = 10;
            double effectiveFeatureHeight = 10;
            double horizontalDistanceFromCrestTop = -2;

            double windDirection = 20;

            var building = new EqualHeightWalls(
                length, width, height);
            var orographyFactor = new HillRidgeOrography(
                actualLengthUpwindSlope,
                actualLengthDownwindSlope,
                effectiveFeatureHeight,
                horizontalDistanceFromCrestTop);
            var terrain = new TerrainCategoryI(orographyFactor);
            var directionalFactor = new DirectionalFactor(windZone, windDirection);
            var buildingSite = new BuildingSite(heightAboveSeaLevel, windZone, terrain,
                directionalFactor: directionalFactor);
            var windLoadData = new WindLoadData(buildingSite, building);
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.ReinforcementConcreteBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    verticalWallOfRectangularBuilding,
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                referenceHeight, calculateStructuralFactor: true);

            //Assert:
            Assert.Multiple(() =>
            {
                // e
                Assert.That(building.EdgeDistance, Is.EqualTo(70));
                // v_b,0
                Assert.That(buildingSite.FundamentalValueBasicWindVelocity, Is.EqualTo(23.32).Within(0.01));
                // c_dir
                Assert.That(directionalFactor.GetFactor(), Is.EqualTo(0.7));
                // v_b
                Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(16.32).Within(0.01));
                // z_e

                // c_r(z_e)
                Assert.That(terrain.GetRoughnessFactorAt(referenceHeight), Is.EqualTo(1.62).Within(0.01));
                // c_0(z_e)
                Assert.That(orographyFactor.GetFactorAt(referenceHeight), Is.EqualTo(1));
                // v_m(z_e)
                Assert.That(windLoadData.GetMeanWindVelocityAt(referenceHeight),
                    Is.EqualTo(26.42).Within(0.01));
                // I_v(z_e)
                Assert.That(windLoadData.GetTurbulenceIntensityAt(referenceHeight),
                    Is.EqualTo(0.109).Within(0.001));
                // q_p(z_e)
                Assert.That(windLoadData.GetPeakVelocityPressureAt(referenceHeight),
                    Is.EqualTo(0.738).Within(0.001));
                // c_sc_d
                Assert.That(structuralFactorCalculator.GetStructuralFactor(true),
                    Is.EqualTo(0.878).Within(0.001));

                Assert.That(result[Field.A], Is.EqualTo(-0.778).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.518).Within(0.001));
                Assert.That(result[Field.C], Is.EqualTo(-0.324).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.518).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.350).Within(0.001));
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

            var building = new EqualHeightWalls(
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
            var verticalWallOfRectangularBuilding = new WallsWindLoads(building, windLoadData);

            var structuralFactorCalculator = new StructuralFactorCalculator(
                building, terrain, windLoadData, StructuralType.ReinforcementConcreteBuilding);

            var externalPressureWindForce =
                new ExternalPressureWindForce(
                    windLoadData,
                    verticalWallOfRectangularBuilding,
                    structuralFactorCalculator);

            //Act:
            var result = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
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

                Assert.That(result[Field.A], Is.EqualTo(-0.756).Within(0.001));
                Assert.That(result[Field.B], Is.EqualTo(-0.504).Within(0.001));
                Assert.That(result[Field.D], Is.EqualTo(0.483).Within(0.001));
                Assert.That(result[Field.E], Is.EqualTo(-0.273).Within(0.001));
            });
        }
    }
}
