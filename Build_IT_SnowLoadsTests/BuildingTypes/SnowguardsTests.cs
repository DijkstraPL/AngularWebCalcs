using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class SnowguardsTests
    {
        [Test()]
        public void SnowguardsTest_Constructor_MinusValues_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(()
                       => new Snowguards(10, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(()
                       => new Snowguards(-10, 1, 0));
            });
        }

        [Test()]
        public void SnowguardsTest_Constructor_Success()
        {
            var snowguards = new Snowguards(10, 30, 0.9);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(10, snowguards.Width);
                Assert.AreEqual(30, snowguards.Slope);
                Assert.AreEqual(0.9, snowguards.SnowLoadOnRoofValue);
            });
        }

        [Test()]
        public void SnowguardsTest_CalculateSnowLoad_Success()
        { 
            var snowguards = new Snowguards(10, 30, 0.9);

            snowguards.CalculateSnowLoad();

            Assert.AreEqual(4.5, Math.Round(snowguards.ForceExertedBySnow, 3));
        }
    }
}