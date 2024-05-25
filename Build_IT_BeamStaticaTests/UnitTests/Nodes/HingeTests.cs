using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class HingeTests
    {
        private Hinge _hinge;

        [SetUp]
        public void SetUp()
        {
            _hinge = new Hinge();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_hinge.DegreesOfFreedom, Is.EqualTo(4));

            Assert.That(_hinge.NormalForce, Is.Null);
            Assert.That(_hinge.ShearForce, Is.Null);
            Assert.That(_hinge.BendingMoment, Is.Null);

            Assert.That(_hinge.HorizontalDeflection, Is.Not.Null);
            Assert.That(_hinge.VerticalDeflection, Is.Not.Null);
            Assert.That(_hinge.LeftRotation, Is.Not.Null);
            Assert.That(_hinge.RightRotation, Is.Not.Null);

            Assert.That(!Is.ReferenceEquals(_hinge.LeftRotation,_hinge.RightRotation));

            Assert.That(_hinge.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _hinge.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(4));

            Assert.That(_hinge.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_hinge.VerticalMovementNumber, Is.EqualTo(1));
            Assert.That(_hinge.LeftRotationNumber, Is.EqualTo(2));
            Assert.That(_hinge.RightRotationNumber, Is.EqualTo(3));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _hinge.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(0));

            Assert.That(_hinge.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_hinge.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_hinge.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_hinge.RightRotationNumber, Is.EqualTo(0));
        }
    }
}
