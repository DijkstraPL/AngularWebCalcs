using Build_IT_WindLoads;
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
    public class NonDimensionalFrequencyTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            var windLoadData = new Mock<IWindLoadData>();
            windLoadData.Setup(b => b.GetMeanWindVelocityAt(2, false)).Returns(1);
            var fundamentalFlexuralFrequency = new Mock<IFactor>();
            fundamentalFlexuralFrequency.Setup(rh => rh.GetFactor()).Returns(3);
            var turbulentLengthScale = new Mock<IFactorAt>();
            turbulentLengthScale.Setup(tls => tls.GetFactorAt(2)).Returns(5);

            var nonDimensionalFrequency = new NonDimensionalFrequency(
                windLoadData.Object, fundamentalFlexuralFrequency.Object, 
                turbulentLengthScale.Object);

            var result = nonDimensionalFrequency.GetFactorAt(2);

            Assert.That(result, Is.EqualTo(15));
        }
    }
}
