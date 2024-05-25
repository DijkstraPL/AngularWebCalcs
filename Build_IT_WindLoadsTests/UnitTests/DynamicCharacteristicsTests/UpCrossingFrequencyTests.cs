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
    public class UpCrossingFrequencyTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            var fundamentalFlexuralFrequency = new Mock<IFactor>();
            fundamentalFlexuralFrequency.Setup(fff => fff.GetFactor()).Returns(2);
            var backgroundFactor = new Mock<IFactor>();
            backgroundFactor.Setup(bf => bf.GetFactor()).Returns(3);
            var resonanceResponse = new Mock<IFactor>();
            resonanceResponse.Setup(rr => rr.GetFactor()).Returns(5);

            var upCrossingFrequency = new UpCrossingFrequency(
                fundamentalFlexuralFrequency.Object,
                backgroundFactor.Object,
                resonanceResponse.Object);

            var result = upCrossingFrequency.GetFactor();

            Assert.That(result, Is.EqualTo(1.581).Within(0.001));
        }
        
        [Test]
        public void GetFactorTest_MinimalValue_Success()
        {
            var fundamentalFlexuralFrequency = new Mock<IFactor>();
            fundamentalFlexuralFrequency.Setup(fff => fff.GetFactor()).Returns(0.1);
            var backgroundFactor = new Mock<IFactor>();
            backgroundFactor.Setup(bf => bf.GetFactor()).Returns(30);
            var resonanceResponse = new Mock<IFactor>();
            resonanceResponse.Setup(rr => rr.GetFactor()).Returns(1);

            var upCrossingFrequency = new UpCrossingFrequency(
                fundamentalFlexuralFrequency.Object,
                backgroundFactor.Object,
                resonanceResponse.Object);

            var result = upCrossingFrequency.GetFactor();

            Assert.That(result, Is.EqualTo(0.08));
        }
    }
}
