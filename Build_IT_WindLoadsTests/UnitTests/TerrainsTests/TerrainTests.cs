using Build_IT_WindLoads;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.Terrains.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.TerrainsTests
{
    [TestFixture]
    public class TerrainTests
    {
        [Test]
        [TestCase(TerrainType.Terrain_0, 0.003, 1, 200)]
        [TestCase(TerrainType.Terrain_I, 0.01, 1, 200)]
        [TestCase(TerrainType.Terrain_II, 0.05, 2,300)]
        [TestCase(TerrainType.Terrain_III, 0.3, 5, 400)]
        public void CreationTests_Success(TerrainType terrainType, 
            double roughnessLength, double minimumHeight, double maximumHeight )
        {
           var terrain = Terrain.Create(terrainType);

           Assert.That(terrain.RoughnessLength, Is.EqualTo(roughnessLength));
           Assert.That(terrain.MinimumHeight, Is.EqualTo(minimumHeight));
           Assert.That(terrain.MaximumHeight, Is.EqualTo(maximumHeight));
        }

        [Test]
        public void CreationTests_TerrainIV_Success()
        {
            var heightDisplacement = new Mock<IFactor>();
            var terrain = Terrain.Create(TerrainType.Terrain_IV, heightDisplacement.Object);

            Assert.That(terrain.RoughnessLength, Is.EqualTo(1));
            Assert.That(terrain.MinimumHeight, Is.EqualTo(10));
            Assert.That(terrain.MaximumHeight, Is.EqualTo(500));
        }

        [Test]
        public void CreationTests_TerrainIV_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Terrain.Create(TerrainType.Terrain_IV));
        }

        [Test]
        [TestCase(TerrainType.Terrain_0, 0.156036)]
        [TestCase(TerrainType.Terrain_II, 0.19)]
        public void RoughnessFactorTest_Success(TerrainType terrainType, double result)
        {
            var terrain = Terrain.Create(terrainType);

            Assert.That(terrain.TerrainFactor, Is.EqualTo(result).Within(0.000001));
        }

        [Test]
        [TestCase(TerrainType.Terrain_0, 2, 1.089072)]
        [TestCase(TerrainType.Terrain_II, 2, 0.760633)]
        [TestCase(TerrainType.Terrain_II, 5, 0.888843)]
        public void GetRoughnessFactorAtTest_Success(TerrainType terrainType, double height, double result)
        {
            var terrain = Terrain.Create(terrainType);

            Assert.That(terrain.GetRoughnessFactorAt(height), Is.EqualTo(result).Within(0.000001));
        }
    }
}
