using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class FixedNodeTests
    {
        private FixedNode _fixedNode;

        [SetUp]
        public void SetUp()
        {
            _fixedNode = new FixedNode();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_fixedNode.DegreesOfFreedom, Is.EqualTo(0));

            Assert.That(_fixedNode.NormalForce, Is.Not.Null);
            Assert.That(_fixedNode.ShearForce, Is.Not.Null);
            Assert.That(_fixedNode.BendingMoment, Is.Not.Null);

            Assert.That(_fixedNode.HorizontalDeflection, Is.Null);
            Assert.That(_fixedNode.VerticalDeflection, Is.Null);
            Assert.That(_fixedNode.LeftRotation, Is.Null);
            Assert.That(_fixedNode.RightRotation, Is.Null);

            Assert.That(_fixedNode.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _fixedNode.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(0));

            Assert.That(_fixedNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_fixedNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_fixedNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_fixedNode.RightRotationNumber, Is.EqualTo(0));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _fixedNode.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(3));

            Assert.That(_fixedNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_fixedNode.VerticalMovementNumber, Is.EqualTo(1));
            Assert.That(_fixedNode.LeftRotationNumber, Is.EqualTo(2));
            Assert.That(_fixedNode.RightRotationNumber, Is.EqualTo(2));
        }
    }
}
