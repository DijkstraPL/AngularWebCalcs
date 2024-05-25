using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads
{
    [TestFixture]
    public class ContinousAngledLoadTests
    {
        private IContinousLoad _continousAngledLoad;
        private ISpan _span;

        [SetUp]
        public void SetUpData()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Length).Returns(10);
            span.Setup(s => s.Section).Returns(new SectionData { Area = 1, MomentOfInteria = 4 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });

            _span = span.Object;

            _continousAngledLoad = ContinuousAngledLoad.Create(
                startPosition: 1, startValue: 2, endPosition: 5, endValue: 10, angle: 30);
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _continousAngledLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-7.866667).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _continousAngledLoad.CalculateSpanLoadVectorNormalForceMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-4.133333).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _continousAngledLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-14.842983).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _continousAngledLoad.CalculateSpanLoadVectorShearMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(-5.941627).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _continousAngledLoad.CalculateSpanLoadVectorBendingMomentMember(_span, leftNode: true);

            Assert.That(result, Is.EqualTo(-28.373302).Within(0.000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _continousAngledLoad.CalculateSpanLoadVectorBendingMomentMember(_span, leftNode: false);

            Assert.That(result, Is.EqualTo(16.198139).Within(0.000001));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _continousAngledLoad.CalculateNormalForce(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _continousAngledLoad.CalculateShear(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _continousAngledLoad.CalculateBendingMoment(distanceFromLoadStartPosition: 5);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _continousAngledLoad.CalculateRotation(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _continousAngledLoad.CalculateHorizontalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _continousAngledLoad.CalculateVerticalDeflection(
                _span, distanceFromLeftSide: 5, currentLength: 1);

            Assert.That(result, Is.Not.EqualTo(0));
        }
    }
}
