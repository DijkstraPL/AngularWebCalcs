using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class ShearLoadTests
    {
        private ISpanLoad _shearLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            _span = span.Object;

            _shearLoad = new ShearLoad(value: -1, position: 1);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _shearLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.972).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _shearLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0.028).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _shearLoad.CalculateSpanLoadBendingMomentMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.81).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _shearLoad.CalculateSpanLoadBendingMomentMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-0.09).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _shearLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _shearLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _shearLoad.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _shearLoad.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _shearLoad.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _shearLoad.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _shearLoad.CalculateShear();

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _shearLoad.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(-5));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _shearLoad.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _shearLoad.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _shearLoad.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
