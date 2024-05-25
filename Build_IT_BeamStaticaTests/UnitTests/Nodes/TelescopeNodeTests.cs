using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class TelescopeNodeTests
    {
        private TelescopeNode _telescopeNode;

        [SetUp]
        public void SetUp()
        {
            _telescopeNode = new TelescopeNode();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_telescopeNode.DegreesOfFreedom, Is.EqualTo(1));

            Assert.That(_telescopeNode.NormalForce, Is.Not.Null);
            Assert.That(_telescopeNode.ShearForce, Is.Null);
            Assert.That(_telescopeNode.BendingMoment, Is.Not.Null);

            Assert.That(_telescopeNode.HorizontalDeflection, Is.Null);
            Assert.That(_telescopeNode.VerticalDeflection, Is.Not.Null);
            Assert.That(_telescopeNode.LeftRotation, Is.Null);
            Assert.That(_telescopeNode.RightRotation, Is.Null);

            Assert.That(_telescopeNode.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _telescopeNode.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(1));

            Assert.That(_telescopeNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_telescopeNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_telescopeNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_telescopeNode.RightRotationNumber, Is.EqualTo(0));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _telescopeNode.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(2));

            Assert.That(_telescopeNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_telescopeNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_telescopeNode.LeftRotationNumber, Is.EqualTo(1));
            Assert.That(_telescopeNode.RightRotationNumber, Is.EqualTo(1));
        }
    }
}
