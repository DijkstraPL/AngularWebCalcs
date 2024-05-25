using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.Factors.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.DynamicCharacteristicsTests
{
    [TestFixture]
    public class ResonanceResponseTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            var referenceHeight = new Mock<IFactor>();
            referenceHeight.Setup(rh => rh.GetFactor()).Returns(2);
            var logarithmicDecrementOfDamping = new Mock<IFactor>();
            logarithmicDecrementOfDamping.Setup(ldd => ldd.GetFactor()).Returns(3);
            var nonDimensionalPowerSpectralDensity = new Mock<IFactorAt>();
            nonDimensionalPowerSpectralDensity.Setup(ndpsd 
                => ndpsd.GetFactorAt(2)).Returns(5);
            var aerodynamicAdmittanceWidth = new Mock<IFactor>();
            aerodynamicAdmittanceWidth.Setup(aaw => aaw.GetFactor()).Returns(7);
            var aerodynamicAdmittanceHeight = new Mock<IFactor>();
            aerodynamicAdmittanceHeight.Setup(aaw => aaw.GetFactor()).Returns(11);

            var resonanceResponse = new ResonanceResponse(
                referenceHeight.Object,
                logarithmicDecrementOfDamping.Object,
                nonDimensionalPowerSpectralDensity.Object,
                aerodynamicAdmittanceWidth.Object,
                aerodynamicAdmittanceHeight.Object);

            var result = resonanceResponse.GetFactor();

            Assert.That(result, Is.EqualTo(633.300).Within(0.001));
        }
    }
}
