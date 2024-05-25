using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests
{
    [TestFixture]
    public class WindLoadDataTests
    {
        [Test]
        public void ReferenceHeightsTest_SmallBuilding_Success()
        {
            var buildingSite = new Mock<IBuildingSite>();
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(5);
            building.Setup(b => b.Width).Returns(10);
            building.Setup(b => b.Length).Returns(15);
            var windLoad = new WindLoadData(buildingSite.Object, building.Object);

            Assert.That(windLoad.GetReferenceHeightAt(0), Is.EqualTo(5));
            Assert.That(windLoad.GetReferenceHeightAt(2), Is.EqualTo(5));
            Assert.That(windLoad.GetReferenceHeightAt(5), Is.EqualTo(5));
        }

        [Test]
        public void ReferenceHeightsTest_MediumBuilding_Success()
        {
            var buildingSite = new Mock<IBuildingSite>();
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(16);
            building.Setup(b => b.Width).Returns(10);
            building.Setup(b => b.Length).Returns(15);
            var windLoad = new WindLoadData(buildingSite.Object, building.Object);

            Assert.That(windLoad.GetReferenceHeightAt(0), Is.EqualTo(10));
            Assert.That(windLoad.GetReferenceHeightAt(10), Is.EqualTo(10));
            Assert.That(windLoad.GetReferenceHeightAt(10.001), Is.EqualTo(16));
            Assert.That(windLoad.GetReferenceHeightAt(16), Is.EqualTo(16));
        }

        [Test]
        public void ReferenceHeightsTest_HeightBuilding_Success()
        {
            var buildingSite = new Mock<IBuildingSite>();
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(30);
            building.Setup(b => b.Width).Returns(10);
            building.Setup(b => b.Length).Returns(15);
            var windLoad = new WindLoadData(buildingSite.Object, building.Object);

            Assert.That(windLoad.GetReferenceHeightAt(0), Is.EqualTo(10));
            Assert.That(windLoad.GetReferenceHeightAt(10), Is.EqualTo(10));
            Assert.That(windLoad.GetReferenceHeightAt(10.001), Is.EqualTo(11));
            Assert.That(windLoad.GetReferenceHeightAt(18), Is.EqualTo(18));
            Assert.That(windLoad.GetReferenceHeightAt(17.5), Is.EqualTo(18));
            Assert.That(windLoad.GetReferenceHeightAt(20), Is.EqualTo(20));
            Assert.That(windLoad.GetReferenceHeightAt(20.001), Is.EqualTo(30));
            Assert.That(windLoad.GetReferenceHeightAt(25), Is.EqualTo(30));
            Assert.That(windLoad.GetReferenceHeightAt(30), Is.EqualTo(30));
        }

        [Test]
        public void GetMeanWindVelocityAtTest_Success()
        {
            var terrainOrography = new Mock<IFactorAt>();
            terrainOrography.Setup(to => to.GetFactorAt(2)).Returns(2);
            var terrain = new Mock<ITerrain>();
            terrain.Setup(t => t.TerrainOrography).Returns(terrainOrography.Object);
            terrain.Setup(t => t.GetRoughnessFactorAt(2)).Returns(3);
            var buildingSite = new Mock<IBuildingSite>();
            buildingSite.Setup(bs => bs.BasicWindVelocity).Returns(5);
            buildingSite.Setup(bs => bs.Terrain).Returns(terrain.Object);
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(2);
            building.Setup(b => b.Width).Returns(3);
            var windLoad = new WindLoadData(buildingSite.Object, building.Object);

            Assert.That(windLoad.GetMeanWindVelocityAt(2), Is.EqualTo(30));
        }

        [Test]
        [TestCase(1, 0.979)]
        [TestCase(5, 0.979)]
        public void GetTurbulenceIntensityAtTest_Success(double minimumHeight, double result )
        {
            var terrainOrography = new Mock<IFactorAt>();
            terrainOrography.Setup(to => to.GetFactorAt(It.IsAny<double>())).Returns(2);
            var terrain = new Mock<ITerrain>();
            terrain.Setup(t => t.RoughnessLength).Returns(3);
            terrain.Setup(t => t.MinimumHeight).Returns(minimumHeight);
            terrain.Setup(t => t.TerrainOrography).Returns(terrainOrography.Object);
            var buildingSite = new Mock<IBuildingSite>();
            buildingSite.Setup(bs => bs.Terrain).Returns(terrain.Object);
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(5);
            building.Setup(b => b.Width).Returns(3);
            var windLoad = new WindLoadData(buildingSite.Object, building.Object);

            Assert.That(windLoad.GetTurbulenceIntensityAt(4), Is.EqualTo(result).Within(0.001));
        }
        
        [Test]
        public void GetPeakVelocityPressureAtTest_Success()
        {
            var terrainOrography = new Mock<IFactorAt>();
            terrainOrography.Setup(to => to.GetFactorAt(It.IsAny<double>())).Returns(2);
            var terrain = new Mock<ITerrain>();
            terrain.Setup(t => t.RoughnessLength).Returns(1);
            terrain.Setup(t => t.MinimumHeight).Returns(1);
            terrain.Setup(t => t.GetRoughnessFactorAt(It.IsAny<double>())).Returns(3);
            terrain.Setup(t => t.TerrainOrography).Returns(terrainOrography.Object);
            var buildingSite = new Mock<IBuildingSite>();
            buildingSite.Setup(bs => bs.BasicWindVelocity).Returns(5);
            buildingSite.Setup(bs => bs.Terrain).Returns(terrain.Object);
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(2);
            building.Setup(b => b.Width).Returns(3);
            var windLoad = new WindLoadData(buildingSite.Object, building.Object);

            Assert.That(windLoad.GetPeakVelocityPressureAt(2), Is.EqualTo(3.403).Within(0.001));
        }
    }
}
