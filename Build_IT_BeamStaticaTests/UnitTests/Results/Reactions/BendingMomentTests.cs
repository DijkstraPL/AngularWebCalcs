using Build_IT_BeamStatica.Results.Reactions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Results.Reactions
{
    [TestFixture]
    public class BendingMomentTests
    {
        [Test]
        public void BendingMomentTest_Construction_Success()
        {
            var bendingMoment = new BendingMoment(15);

            Assert.That(bendingMoment.Position, Is.EqualTo(15));
        }

        [Test]
        public void BendingMomentTest_ConstructionWithNullPosition_Success()
        {
            var bendingMoment = new BendingMoment();

            Assert.That(bendingMoment.Position, Is.Null);
        }


        [Test]
        public void BendingMomentTest_SetValue_Success()
        {
            var bendingMoment = new BendingMoment();
            bendingMoment.Value = 5;

            Assert.That(bendingMoment.Value, Is.EqualTo(5));
        }

        [Test]
        public void BendingMomentTest_ToString_Success()
        {
            var bendingMoment = new BendingMoment();
            bendingMoment.Value = 5;

            Assert.That(bendingMoment.Value.ToString(), Is.EqualTo("5"));
        }
    }
}
