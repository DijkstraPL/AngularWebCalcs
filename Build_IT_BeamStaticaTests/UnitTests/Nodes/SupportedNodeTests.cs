using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class SupportedNodeTests
    {
        private SupportedNode _supportedNode;

        [SetUp]
        public void SetUp()
        {
            _supportedNode = new SupportedNode();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_supportedNode.DegreesOfFreedom, Is.EqualTo(1));

            Assert.That(_supportedNode.NormalForce, Is.Not.Null);
            Assert.That(_supportedNode.ShearForce, Is.Not.Null);
            Assert.That(_supportedNode.BendingMoment, Is.Null);

            Assert.That(_supportedNode.HorizontalDeflection, Is.Null);
            Assert.That(_supportedNode.VerticalDeflection, Is.Null);
            Assert.That(_supportedNode.LeftRotation, Is.Not.Null);
            Assert.That(_supportedNode.RightRotation, Is.Not.Null);

            Assert.That(Is.ReferenceEquals(_supportedNode.LeftRotation,_supportedNode.RightRotation));

            Assert.That(_supportedNode.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _supportedNode.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(1));

            Assert.That(_supportedNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_supportedNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_supportedNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_supportedNode.RightRotationNumber, Is.EqualTo(0));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _supportedNode.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(2));

            Assert.That(_supportedNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_supportedNode.VerticalMovementNumber, Is.EqualTo(1));
            Assert.That(_supportedNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_supportedNode.RightRotationNumber, Is.EqualTo(0));
        }
    }
}
