using Build_IT_BeamStatica.Results.Displacements;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Results.Displacements
{
    [TestFixture]
    public class VerticalDeflectionTests
    {
        [Test]
        public void VerticalDeflectionTest_Construction_Success()
        {
            var verticalDeflection = new VerticalDeflection(15);

            Assert.That(verticalDeflection.Position, Is.EqualTo(15));
        }

        [Test]
        public void VerticalDeflectionTest_ConstructionWithNullPosition_Success()
        {
            var verticalDeflection = new VerticalDeflection();

            Assert.That(verticalDeflection.Position, Is.Null);
        }


        [Test]
        public void VerticalDeflectionTest_SetValue_Success()
        {
            var verticalDeflection = new VerticalDeflection();
            verticalDeflection.Value = 5;

            Assert.That(verticalDeflection.Value, Is.EqualTo(5));
        }

        [Test]
        public void VerticalDeflectionTest_ToString_Success()
        {
            var verticalDeflection = new VerticalDeflection();
            verticalDeflection.Value = 5;

            Assert.That(verticalDeflection.Value.ToString(), Is.EqualTo("5"));
        }
    }
}
