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
    public class StructuralFactorTests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            //Arrange:
            var sizeFactor = Mock.Of<IFactor>(sf => sf.GetFactor() == 2);
            var dynamicFactor = Mock.Of<IFactor>(df => df.GetFactor() == 3);
            var structuralFactor = new StructuralFactor(sizeFactor, dynamicFactor);

            //Act:
            var result = structuralFactor.GetFactor();

            //Assert:
            Assert.That(result, Is.EqualTo(6));
        }
    }
}
