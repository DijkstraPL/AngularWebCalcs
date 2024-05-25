using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads
{
    [TestFixture]
    public class SpanExtendLoadTests
    {
        private IContinousLoad _spanExtendLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            span.Setup(s => s.Section).Returns(new SectionData { Area = 5 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });

            _span = span.Object;

            _spanExtendLoad = SpanExtendLoad.Create(_span, lengthIncrease: 10);
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _spanExtendLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _spanExtendLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _spanExtendLoad.CalculateSpanLoadVectorShearMember(
                _span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _spanExtendLoad.CalculateSpanLoadVectorShearMember(
                _span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _spanExtendLoad.CalculateSpanLoadVectorBendingMomentMember(
                _span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _spanExtendLoad.CalculateSpanLoadVectorBendingMomentMember(
                _span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _spanExtendLoad.CalculateNormalForce(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _spanExtendLoad.CalculateShear(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _spanExtendLoad.CalculateBendingMoment(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _spanExtendLoad.CalculateRotation(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _spanExtendLoad.CalculateHorizontalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _spanExtendLoad.CalculateVerticalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
