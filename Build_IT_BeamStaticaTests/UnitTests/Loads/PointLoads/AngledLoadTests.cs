using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class AngledLoadTests
    {
        private ISpanLoad _angledLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            _span = span.Object;

            _angledLoad = new AngledLoad(value: -1, position: 1, angle:30);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _angledLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.841777).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _angledLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0.024249).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _angledLoad.CalculateSpanLoadBendingMomentMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.701481).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _angledLoad.CalculateSpanLoadBendingMomentMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-0.077942).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _angledLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0.45).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _angledLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0.05).Within(0.000001));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _angledLoad.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(-0.5).Within(0.000001));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _angledLoad.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(-0.866025).Within(0.000001));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _angledLoad.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _angledLoad.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(-0.5).Within(0.000001));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _angledLoad.CalculateShear();

            Assert.That(result, Is.EqualTo(-0.866025).Within(0.000001));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _angledLoad.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(-4.330127).Within(0.000001));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _angledLoad.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _angledLoad.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _angledLoad.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
