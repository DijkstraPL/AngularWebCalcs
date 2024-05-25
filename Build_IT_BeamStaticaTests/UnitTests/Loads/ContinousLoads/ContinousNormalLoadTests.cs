using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads
{
    [TestFixture]
    public class ContinousNormalLoadTests
    {
        private IContinousLoad _continousNormalLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            span.Setup(s => s.Section).Returns(new  SectionData { Area = 2 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 3 });

            _span = span.Object;

            _continousNormalLoad = ContinuousNormalLoad.Create(
                startPosition: 1, startValue: 2, endPosition: 5, endValue: 10);
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _continousNormalLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-15.733333).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _continousNormalLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-8.266667).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _continousNormalLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _continousNormalLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _continousNormalLoad.CalculateSpanLoadVectorBendingMomentMember(
                _span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _continousNormalLoad.CalculateSpanLoadVectorBendingMomentMember(
                _span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _continousNormalLoad.CalculateNormalForce(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _continousNormalLoad.CalculateShear(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _continousNormalLoad.CalculateBendingMoment(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _continousNormalLoad.CalculateRotation(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _continousNormalLoad.CalculateHorizontalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _continousNormalLoad.CalculateVerticalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
