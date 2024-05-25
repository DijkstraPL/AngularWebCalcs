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
    public class DynamicFactorTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            //Arrange:
            var referenceHeight = Mock.Of<IFactor>(rh => rh.GetFactor() == 2);
            var windLoadData = Mock.Of<IWindLoadData>(wld 
                => wld.GetTurbulenceIntensityAt(2, false) == 3);
            var peakFactor = Mock.Of<IFactor>(f => f.GetFactor() == 7);
            var backgroundFactor = Mock.Of<IFactor>(bf => bf.GetFactor() == 5);
            var resonanceResponse = Mock.Of<IFactor>(bf => bf.GetFactor() == 11);

            var dynamicFactor = new DynamicFactor(
                referenceHeight,
                windLoadData,
                peakFactor,
                backgroundFactor,
                resonanceResponse);

            //Act:
            var result = dynamicFactor.GetFactor();

            //Assert:
            Assert.That(result, Is.EqualTo(3.523959).Within(0.000001));
        }
    }
}
