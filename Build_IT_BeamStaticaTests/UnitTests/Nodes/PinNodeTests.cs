using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class PinNodeTests
    {
        private PinNode _pinNode;

        [SetUp]
        public void SetUp()
        {
            _pinNode = new PinNode();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_pinNode.DegreesOfFreedom, Is.EqualTo(2));

            Assert.That(_pinNode.NormalForce, Is.Null);
            Assert.That(_pinNode.ShearForce, Is.Not.Null);
            Assert.That(_pinNode.BendingMoment, Is.Null);

            Assert.That(_pinNode.HorizontalDeflection, Is.Not.Null);
            Assert.That(_pinNode.VerticalDeflection, Is.Null);
            Assert.That(_pinNode.LeftRotation, Is.Not.Null);
            Assert.That(_pinNode.RightRotation, Is.Not.Null);

            Assert.That(Is.ReferenceEquals(_pinNode.LeftRotation,_pinNode.RightRotation));

            Assert.That(_pinNode.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _pinNode.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(2));

            Assert.That(_pinNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_pinNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_pinNode.LeftRotationNumber, Is.EqualTo(1));
            Assert.That(_pinNode.RightRotationNumber, Is.EqualTo(1));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _pinNode.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(1));

            Assert.That(_pinNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_pinNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_pinNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_pinNode.RightRotationNumber, Is.EqualTo(0));
        }
    }
}
