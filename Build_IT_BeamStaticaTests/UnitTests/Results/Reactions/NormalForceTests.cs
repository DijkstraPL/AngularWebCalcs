using Build_IT_BeamStatica.Results.Reactions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Results.Reactions
{
    [TestFixture]
    public class NormalForceTests
    {
        [Test]
        public void NormalForceTest_Construction_Success()
        {
            var normalForce = new NormalForce(15);

            Assert.That(normalForce.Position, Is.EqualTo(15));
        }

        [Test]
        public void NormalForceTest_ConstructionWithNullPosition_Success()
        {
            var normalForce = new NormalForce();

            Assert.That(normalForce.Position, Is.Null);
        }


        [Test]
        public void NormalForceTest_SetValue_Success()
        {
            var normalForce = new NormalForce();
            normalForce.Value = 5;

            Assert.That(normalForce.Value, Is.EqualTo(5));
        }

        [Test]
        public void NormalForceTest_ToString_Success()
        {
            var normalForce = new NormalForce();
            normalForce.Value = 5;

            Assert.That(normalForce.Value.ToString(), Is.EqualTo("5"));
        }
    }
}
