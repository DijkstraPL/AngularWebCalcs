using Build_IT_WindLoads.TerrainOrographies;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.TerrainOrographiesTests
{
    [TestFixture]
    public class HillRidgeOrographyTests
    {
        [Test]
        [TestCase(21, 10, 1, -1, 1)]
        [TestCase(19, 10, 1, -1, 1.084)]
        [TestCase(17, 10, 5, -2, 1.393)]
        [TestCase(170, 10, 50, -20, 1.432)]
        [TestCase(170, 10, 50, -80, 1.170)]
        [TestCase(100, 10, 5.1, -10, 1.078)]
        [TestCase(20, 10, 10, 10, 1.101)]
        [TestCase(20, 10, 10, 5, 1.241)]
        [TestCase(10, 40, 15, 2, 1.536)]
        public void GetOrographicFactorAtTest_Success(
            double actualLengthUpwindSlope,
            double actualLengthDownwindSlope,
            double effectiveFeatureHeight,
            double horizontalDistanceFromCrestTop,
            double expectedResult)
        {
            var hillRidgeOrography = new HillRidgeOrography(
                actualLengthUpwindSlope,
                actualLengthDownwindSlope,
                effectiveFeatureHeight,
                horizontalDistanceFromCrestTop);

            Assert.That(hillRidgeOrography.GetFactorAt(verticalDistanceFromCrestTop: 1),
                Is.EqualTo(expectedResult).Within(0.001));
        }
    }
}
