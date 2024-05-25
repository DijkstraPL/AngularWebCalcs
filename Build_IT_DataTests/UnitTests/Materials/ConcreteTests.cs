using Build_IT_Data.Materials;
using NUnit.Framework;
using System;

namespace Build_IT_DataTests.UnitTests.Materials
{
    [TestFixture]
    public class ConcreteTests
    {
        [Test]
        public void ConcreteCreationTest_MinusYoungModulus_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()
                => new Concrete(youngModulus: -30, withReinforcement: true));
        }

        [Test]
        public void ConcreteCreationTest_WithoutReinforcement_ThermalExpansionCoefficientIsSet()
        {
            var concrete = new Concrete(youngModulus: 25, withReinforcement: false);

            Assert.That(concrete.ThermalExpansionCoefficient, Is.EqualTo(0.000010));
        }

        [Test]
        public void ConcreteCreationTest_WithoutReinforcement_YoungModulusIsSet()
        {
            var concrete = new Concrete(youngModulus: 25, withReinforcement: false);

            Assert.That(concrete.YoungModulus, Is.EqualTo(25));
        }

        [Test]
        public void ConcreteCreationTest_WithReinforcement_DensityShouldBe2500()
        {
            var concrete = new Concrete(youngModulus: 30, withReinforcement: true);

            Assert.That(concrete.Density, Is.EqualTo(2500));
        }

        [Test]
        public void ConcreteCreationTest_WithoutReinforcement_DensityShouldBe2400()
        {
            var concrete = new Concrete(youngModulus: 25, withReinforcement: false);

            Assert.That(concrete.Density, Is.EqualTo(2400));
        }
    }
}
