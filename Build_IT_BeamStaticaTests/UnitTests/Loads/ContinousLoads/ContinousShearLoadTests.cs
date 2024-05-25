using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads
{
    [TestFixture]
    public class ContinousShearLoadTests
    {
        private IContinousLoad _continousShearLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10); 
            span.Setup(s => s.Section).Returns(new SectionData { MomentOfInteria = 3 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });

            _span = span.Object;

            _continousShearLoad = ContinuousShearLoad.Create(
                startPosition: 1, startValue: 2, endPosition: 5, endValue: 10);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _continousShearLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-17.1392).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _continousShearLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-6.8608).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _continousShearLoad.CalculateSpanLoadVectorBendingMomentMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-32.762667).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _continousShearLoad.CalculateSpanLoadVectorBendingMomentMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(18.704).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _continousShearLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _continousShearLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _continousShearLoad.CalculateNormalForce(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _continousShearLoad.CalculateShear(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _continousShearLoad.CalculateBendingMoment(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _continousShearLoad.CalculateRotation(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _continousShearLoad.CalculateHorizontalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _continousShearLoad.CalculateVerticalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }
    }
}
