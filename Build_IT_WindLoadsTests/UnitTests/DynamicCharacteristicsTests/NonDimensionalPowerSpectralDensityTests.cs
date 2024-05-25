using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.Factors.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.DynamicCharacteristicsTests
{
    [TestFixture]
    public class NonDimensionalPowerSpectralDensityTests
    {
        [Test]
        public void GetFactorAtTest_Success()
        {
            var nonDimensionalFrequency = new Mock<IFactorAt>();
            nonDimensionalFrequency.Setup(ndf => ndf.GetFactorAt(2)).Returns(3);

            var nonDimensionalPowerSpectralDensity =
                new NonDimensionalPowerSpectralDensity(nonDimensionalFrequency.Object);

            var result = nonDimensionalPowerSpectralDensity.GetFactorAt(2);

            Assert.That(result, Is.EqualTo(0.064588).Within(0.000001));
        }
    }
}
