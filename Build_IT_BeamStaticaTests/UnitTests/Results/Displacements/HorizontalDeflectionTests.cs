using Build_IT_BeamStatica.Results.Displacements;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Results.Displacements
{
    [TestFixture]
    public class HorizontalDeflectionTests
    {
        [Test]
        public void HorizontalDeflectionTest_Construction_Success()
        {
            var horizontalDeflection = new HorizontalDeflection(15);

            Assert.That(horizontalDeflection.Position, Is.EqualTo(15));
        }

        [Test]
        public void HorizontalDeflectionTest_ConstructionWithNullPosition_Success()
        {
            var horizontalDeflection = new HorizontalDeflection();

            Assert.That(horizontalDeflection.Position, Is.Null);
        }


        [Test]
        public void HorizontalDeflectionTest_SetValue_Success()
        {
            var horizontalDeflection = new HorizontalDeflection();
            horizontalDeflection.Value = 5;

            Assert.That(horizontalDeflection.Value, Is.EqualTo(5));
        }

        [Test]
        public void HorizontalDeflectionTest_ToString_Success()
        {
            var horizontalDeflection = new HorizontalDeflection();
            horizontalDeflection.Value = 5;

            Assert.That(horizontalDeflection.Value.ToString(), Is.EqualTo("5"));
        }
    }
}
