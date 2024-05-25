using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.Terrains.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.DynamicCharacteristicsTests
{
    [TestFixture]
    public class TurbulentLengthScaleTests
    {
        [Test]
        [TestCase(1, 2, 10.648)]
        [TestCase(2, 1, 10.648)]
        public void GetFactorTest_Success(double minimumHeight, 
            double height, double expectedResult)
        {
            var terrain = new Mock<ITerrain>();
            terrain.Setup(t => t.MinimumHeight).Returns(minimumHeight);
            terrain.Setup(t => t.RoughnessLength).Returns(3);
            var turbulentLengthScale = new TurbulentLengthScale(terrain.Object);

            var result = turbulentLengthScale.GetFactorAt(height);

            Assert.That(result, Is.EqualTo(expectedResult).Within(0.001));
        }

        [Test]
        public void GetFactorTest_Above200_ThrowsArgumentOutOfRangeException()
        {
            var terrain = new Mock<ITerrain>();
            terrain.Setup(t => t.MinimumHeight).Returns(1);
            terrain.Setup(t => t.RoughnessLength).Returns(3);
            var turbulentLengthScale = new TurbulentLengthScale(terrain.Object);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => turbulentLengthScale.GetFactorAt(201));
        }
    }
}
