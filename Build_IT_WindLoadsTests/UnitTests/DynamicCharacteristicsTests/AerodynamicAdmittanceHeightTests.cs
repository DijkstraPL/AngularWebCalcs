using Build_IT_WindLoads.BuildingData.Interfaces;
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
    public class AerodynamicAdmittanceHeightTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(1);
            var referenceHeight = new Mock<IFactor>();
            referenceHeight.Setup(rh => rh.GetFactor()).Returns(2);
            var turbulentLengthScale = new Mock<IFactorAt>();
            turbulentLengthScale.Setup(tls => tls.GetFactorAt(2)).Returns(5);
            var nondimensionalFrequency = new Mock<IFactorAt>();
            nondimensionalFrequency.Setup(nf => nf.GetFactorAt(2)).Returns(7);


            var aerodynamicAdmittanceHeight = new AerodynamicAdmittanceHeight(
                building.Object,
                referenceHeight.Object,
                turbulentLengthScale.Object,
                nondimensionalFrequency.Object);

            var result = aerodynamicAdmittanceHeight.GetFactor();

            Assert.That(result, Is.EqualTo(0.143).Within(0.001));
        }
    }
}
