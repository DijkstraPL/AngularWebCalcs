using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class NormalLoadTests
    {
        private ISpanLoad _normalLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            _span = span.Object;

            _normalLoad = new NormalLoad(value: -1, position: 1);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _normalLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _normalLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _normalLoad.CalculateSpanLoadBendingMomentMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _normalLoad.CalculateSpanLoadBendingMomentMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _normalLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.9));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _normalLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0.1));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _normalLoad.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _normalLoad.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _normalLoad.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _normalLoad.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _normalLoad.CalculateShear();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _normalLoad.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _normalLoad.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _normalLoad.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _normalLoad.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
