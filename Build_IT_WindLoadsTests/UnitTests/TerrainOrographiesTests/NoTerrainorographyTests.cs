using Build_IT_WindLoads.TerrainOrographies;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.TerrainOrographiesTests
{
    [TestFixture]
    public class NoTerrainOrographyTests
    {
        [Test]
        public void GetFactor_Returns1()
        {
            var noTerrainOrography = new NoTerrainOrography();

            var result = noTerrainOrography.GetFactorAt(1245);

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
