using Build_IT_WindLoads;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.Factors.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.FactorsTests
{
    [TestFixture]
    public class SizeFactorTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            //Arrange:
            var referenceHeight = new Mock<IFactor>();
            referenceHeight.Setup(rh => rh.GetFactor()).Returns(2);
            var windLoadData = new Mock<IWindLoadData>();
            windLoadData.Setup(wld => wld.GetTurbulenceIntensityAt(2, false)).Returns(3);
            var backgroundFactor = new Mock<IFactor>();
            backgroundFactor.Setup(bf => bf.GetFactor()).Returns(5);

            var sizeFactor = new SizeFactor(referenceHeight.Object,
                windLoadData.Object,
                backgroundFactor.Object);

            //Act:
            var result = sizeFactor.GetFactor();

            //Assert:
            Assert.That(result, Is.EqualTo(2.179883).Within(0.000001));
        }
    }
}
