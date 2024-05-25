using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class BendingMomentTests
    {
        private ISpanLoad _bendingMoment;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            _span = span.Object;

            _bendingMoment = new BendingMoment(value: -1, position: 1);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _bendingMoment.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.054).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _bendingMoment.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-0.054).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _bendingMoment.CalculateSpanLoadBendingMomentMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-0.63).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _bendingMoment.CalculateSpanLoadBendingMomentMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0.17).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _bendingMoment.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _bendingMoment.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _bendingMoment.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _bendingMoment.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _bendingMoment.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _bendingMoment.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _bendingMoment.CalculateShear();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _bendingMoment.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _bendingMoment.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _bendingMoment.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _bendingMoment.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
