using Build_IT_CommonTools;
using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests
{
    [TestFixture]
    public class BuildingSiteTests
    {
        [Test]
        public void ConstructionTest_Success()
        {
            var terrain = new Mock<ITerrain>();
            var buildingSite = new BuildingSite(heightAboveSeaLevel: 250, windZone: WindZone.II, 
                terrain: terrain.Object);

            Assert.That(buildingSite.HeightAboveSeaLevel, Is.EqualTo(250));
            Assert.That(buildingSite.WindZone, Is.EqualTo(WindZone.II));
        }

        [Test]
        [TestCase(250, WindZone.I, 22)]
        [TestCase(250, WindZone.II, 26)]
        [TestCase(250, WindZone.III, 22)]
        [TestCase(350, WindZone.I, 22.66)]
        [TestCase(350, WindZone.II, 26)]
        [TestCase(350, WindZone.III, 22.66)]
        [TestCase(250, WindZone.I_II, 24)]
        public void FundamentalValueBasicWindVelocityTest_Success(
            double heightAboveSeaLevel, WindZone windZone, double expectedResult)
        {
            var terrain = new Mock<ITerrain>();
            var buildingSite = new BuildingSite(heightAboveSeaLevel, windZone, terrain.Object);

            Assert.That(buildingSite.FundamentalValueBasicWindVelocity, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SeasonalFactorTest_Success()
        {
            var terrain = new Mock<ITerrain>();
            var buildingSite = new BuildingSite(heightAboveSeaLevel: 250, windZone: WindZone.II, 
                terrain: terrain.Object);

            Assert.That(buildingSite.SeasonalFactor, Is.EqualTo(1.0));
        }

        [Test]
        public void DirectionalFactorTest_Success()
        {
            var terrain = new Mock<ITerrain>();
            var buildingSite = new BuildingSite(heightAboveSeaLevel: 250, windZone: WindZone.II,
                terrain: terrain.Object);

            Assert.That(buildingSite.DirectionalFactor, Is.EqualTo(1.0));
        }

        [Test]
        public void BasicWindVelocityTest_Success()
        {
            var terrain = new Mock<ITerrain>();
            var buildingSite = new BuildingSite(heightAboveSeaLevel: 350, windZone: WindZone.II,
                terrain: terrain.Object);

            Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(26));
        }

        [Test]
        public void BasicWindVelocityTest_WithFactors_Success()
        {
            var terrain = new Mock<ITerrain>();
            var seasonalFactor = new Mock<IFactor>();
            seasonalFactor.Setup(sf => sf.GetFactor()).Returns(0.5);
            var directionalFactor = new Mock<IFactor>();
            directionalFactor.Setup(sf => sf.GetFactor()).Returns(0.1);
            var buildingSite = new BuildingSite(
                heightAboveSeaLevel: 250, 
                windZone: WindZone.I,
                terrain: terrain.Object, 
                seasonalFactor: seasonalFactor.Object, 
                directionalFactor: directionalFactor.Object );

            Assert.That(buildingSite.BasicWindVelocity, Is.EqualTo(1.1));
        }
    }
}
