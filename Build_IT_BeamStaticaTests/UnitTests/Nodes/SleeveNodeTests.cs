using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class SleeveNodeTests
    {
        private SleeveNode _sleeveNode;

        [SetUp]
        public void SetUp()
        {
            _sleeveNode = new SleeveNode();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_sleeveNode.DegreesOfFreedom, Is.EqualTo(1));

            Assert.That(_sleeveNode.NormalForce, Is.Null);
            Assert.That(_sleeveNode.ShearForce, Is.Not.Null);
            Assert.That(_sleeveNode.BendingMoment, Is.Not.Null);

            Assert.That(_sleeveNode.HorizontalDeflection, Is.Not.Null);
            Assert.That(_sleeveNode.VerticalDeflection, Is.Null);
            Assert.That(_sleeveNode.LeftRotation, Is.Null);
            Assert.That(_sleeveNode.RightRotation, Is.Null);
            
            Assert.That(_sleeveNode.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _sleeveNode.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(1));

            Assert.That(_sleeveNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_sleeveNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_sleeveNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_sleeveNode.RightRotationNumber, Is.EqualTo(0));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _sleeveNode.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(2));

            Assert.That(_sleeveNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_sleeveNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_sleeveNode.LeftRotationNumber, Is.EqualTo(1));
            Assert.That(_sleeveNode.RightRotationNumber, Is.EqualTo(1));
        }
    }
}
