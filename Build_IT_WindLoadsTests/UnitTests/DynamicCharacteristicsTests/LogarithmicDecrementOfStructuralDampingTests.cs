using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.DynamicCharacteristics.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.DynamicCharacteristicsTests
{
    [TestFixture]
    public class LogarithmicDecrementOfStructuralDampingTests
    {
        [Test]
        [TestCase(StructuralType.ReinforcementConcreteBuilding, 0.1)]
        [TestCase(StructuralType.SteelBuilding, 0.05)]
        [TestCase(StructuralType.MixedStructuresConcrete_Steel, 0.08)]
        [TestCase(StructuralType.ReinforcementConcreteTowerOrChimney, 0.03)]
        public void GetFactorTest_Success(
            StructuralType structuralType, double expectedResult)
        {
            var logarithmicDecrementOfStructuralDamping =
                new LogarithmicDecrementOfStructuralDamping(structuralType);

            var result = logarithmicDecrementOfStructuralDamping.GetFactor();

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
