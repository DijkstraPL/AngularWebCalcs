using Build_IT_BeamStatica.Results.Reactions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Results.Reactions
{
    [TestFixture]
    public class ShearForceTests
    {
        [Test]
        public void ShearForceTest_Construction_Success()
        {
            var shearForce = new ShearForce(15);

            Assert.That(shearForce.Position, Is.EqualTo(15));
        }

        [Test]
        public void ShearForceTest_ConstructionWithNullPosition_Success()
        {
            var shearForce = new ShearForce();

            Assert.That(shearForce.Position, Is.Null);
        }


        [Test]
        public void ShearForceTest_SetValue_Success()
        {
            var shearForce = new ShearForce();
            shearForce.Value = 5;

            Assert.That(shearForce.Value, Is.EqualTo(5));
        }

        [Test]
        public void ShearForceTest_ToString_Success()
        {
            var shearForce = new ShearForce();
            shearForce.Value = 5;

            Assert.That(shearForce.Value.ToString(), Is.EqualTo("5"));
        }
    }
}
