using Build_IT_WindLoads.TerrainOrographies;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.TerrainOrographiesTests
{
    [TestFixture]
    public class CliffEscarpmentOrographyTests
    {
        [Test]
        public void ConstructorTest_Success()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope:1,
                effectiveFeatureHeight: 3,
                horizontalDistanceFromCrestTop: 4);

            Assert.That(cliffEscarpmentOrography.ActualLengthUpwindSlope, Is.EqualTo(1));
            Assert.That(cliffEscarpmentOrography.EffectiveFeatureHeight, Is.EqualTo(3));
            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop, Is.EqualTo(4));
        }

        [Test]
        public void UpwindSlopeTest_Success()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 2,
                effectiveFeatureHeight: 4,
                horizontalDistanceFromCrestTop: 5);

            Assert.That(cliffEscarpmentOrography.UpwindSlope, Is.EqualTo(2));
        }

        [Test]
        public void EffectiveLengthUpwindSlopeTest_UpwindSlopeGreaterThan0_3_Success()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 2,
                effectiveFeatureHeight: 6,
                horizontalDistanceFromCrestTop: 5);

            Assert.That(cliffEscarpmentOrography.EffectiveLengthUpwindSlope, Is.EqualTo(20));
        }

        [Test]
        public void EffectiveLengthUpwindSlopeTest_UpwindSlopeSmallerThan0_3_Success()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 5,
                effectiveFeatureHeight: 1,
                horizontalDistanceFromCrestTop: 5);

            Assert.That(cliffEscarpmentOrography.EffectiveLengthUpwindSlope, Is.EqualTo(5));
        }

        [Test]
        public void EffectiveLengthUpwindSlopeTest_UpwindSlopeSmallerThan0_05_ThrowsArgumentOutOfRangeException()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 5,
                effectiveFeatureHeight: 0,
                horizontalDistanceFromCrestTop: 5);

            Assert.Throws<ArgumentOutOfRangeException>(() 
                => { var result = cliffEscarpmentOrography.EffectiveLengthUpwindSlope; });
        }

        [Test]
        public void GetOrographicFactorAtTest_OrographicFactorNotNeeded_Upwind_FirstStatement_Returns1()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 5,
                effectiveFeatureHeight: 0,
                horizontalDistanceFromCrestTop: -1);

            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop < 0, Is.True);
            Assert.That(cliffEscarpmentOrography.UpwindSlope > 0.05, Is.False);
            Assert.That(cliffEscarpmentOrography.UpwindSlope <= 0.3, Is.True);
            Assert.That(
                Math.Abs(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop) 
                <= cliffEscarpmentOrography.ActualLengthUpwindSlope / 2, Is.True);

            Assert.That(cliffEscarpmentOrography.GetFactorAt(verticalDistanceFromCrestTop: 1), 
                Is.EqualTo(1));
        }

        [Test]
        public void GetOrographicFactorAtTest_OrographicFactorNotNeeded_Upwind_SecondStatement_Returns1()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 5,
                effectiveFeatureHeight: 1,
                horizontalDistanceFromCrestTop: -10);

            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop < 0, Is.True);
            Assert.That(cliffEscarpmentOrography.UpwindSlope > 0.05, Is.True);
            Assert.That(cliffEscarpmentOrography.UpwindSlope <= 0.3, Is.True);
            Assert.That(
                Math.Abs(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop)
                <= cliffEscarpmentOrography.ActualLengthUpwindSlope / 2, Is.False);

            Assert.That(cliffEscarpmentOrography.GetFactorAt(verticalDistanceFromCrestTop: 1), 
                Is.EqualTo(1));
        }

        [Test]
        public void GetOrographicFactorAtTest_OrographicFactorNotNeeded_Downwind_FirstStatement_Returns1()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 4,
                effectiveFeatureHeight: 1,
                horizontalDistanceFromCrestTop: 7);

            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop >= 0, Is.True);
            Assert.That(cliffEscarpmentOrography.UpwindSlope < 0.3, Is.True);
            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop 
                < 1.5 * cliffEscarpmentOrography.EffectiveLengthUpwindSlope, Is.False);


            Assert.That(cliffEscarpmentOrography.GetFactorAt(verticalDistanceFromCrestTop: 1), 
                Is.EqualTo(1));
        }

        [Test]
        public void GetOrographicFactorAtTest_OrographicFactorNotNeeded_Downwind_SecondStatement_Returns1()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 4,
                effectiveFeatureHeight: 2,
                horizontalDistanceFromCrestTop: 11);

            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop >= 0, Is.True);
            Assert.That(cliffEscarpmentOrography.UpwindSlope >= 0.3, Is.True);
            Assert.That(cliffEscarpmentOrography.HorizontalDistanceFromCrestTop
                < 5 * cliffEscarpmentOrography.EffectiveFeatureHeight, Is.False);


            Assert.That(cliffEscarpmentOrography.GetFactorAt(verticalDistanceFromCrestTop: 1),
                Is.EqualTo(1));
        }

        [Test]
        public void GetOrographicFactorAtTest_Returns1()
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope: 21,
                effectiveFeatureHeight: 1,
                horizontalDistanceFromCrestTop: -1);

            Assert.That(cliffEscarpmentOrography.GetFactorAt(verticalDistanceFromCrestTop: 1), 
                Is.EqualTo(1));
        }

        [Test]
        [TestCase(21, 1, -1, 1)]
        [TestCase(19, 1, -1, 1.084)]
        [TestCase(17, 5, -2, 1.393)]
        [TestCase(170, 50, -20, 1.432)]
        [TestCase(170, 50, -80, 1.170)]
        [TestCase(100, 5.1, -10, 1.078)]
        [TestCase(20, 10, 10, 1.373)]
        [TestCase(5, 20, 7, 1.508)]
        public void GetOrographicFactorAtTest_Success(
            double actualLengthUpwindSlope,
            double effectiveFeatureHeight,
            double horizontalDistanceFromCrestTop,
            double expectedResult)
        {
            var cliffEscarpmentOrography = new CliffEscarpmentOrography(
                actualLengthUpwindSlope,
                effectiveFeatureHeight,
                horizontalDistanceFromCrestTop);

            Assert.That(cliffEscarpmentOrography.GetFactorAt(verticalDistanceFromCrestTop: 1),
                Is.EqualTo(expectedResult).Within(0.001));
        }
    }
}
