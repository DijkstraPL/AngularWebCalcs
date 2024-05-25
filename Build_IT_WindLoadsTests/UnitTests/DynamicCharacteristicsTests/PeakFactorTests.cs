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
    public class PeakFactorTests
    {
        [Test]
        [TestCase(2,3.925)]
        [TestCase(0.01,3)]
        public void GetFactorTest_Success(
            double upCrossingFrequencyValue,
            double expectedResult)
        {
            var upCrossingFrequency = new Mock<IFactor>();
            upCrossingFrequency.Setup(ucf => ucf.GetFactor()).Returns(upCrossingFrequencyValue);
            
            var peakFactor = new PeakFactor(upCrossingFrequency.Object);

            var result = peakFactor.GetFactor();

            Assert.That(result, Is.EqualTo(expectedResult).Within(0.001));
        }
    }
}
