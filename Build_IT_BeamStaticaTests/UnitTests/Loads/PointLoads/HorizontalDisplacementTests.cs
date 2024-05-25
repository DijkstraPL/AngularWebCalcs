using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class HorizontalDisplacementTests
    {
        private ISpanLoad _horizontalDisplacement;
        private Mock<ISpan> _span;

        [SetUp]
        public void SetUpData()
        {
            _span = new Mock<ISpan>();
            _span.Setup(s => s.Length).Returns(10);
            _span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus=2 });
            _span.Setup(s => s.Section).Returns(new SectionData { Area = 3 });

            _horizontalDisplacement = new HorizontalDisplacement(value: -100);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_Success()
        {
            var result = _horizontalDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_Success()
        {
            var result = _horizontalDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_Success()
        {
            var result = _horizontalDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_Success()
        {
            var result = _horizontalDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_horizontalDisplacement)).Returns(false);
            var result = _horizontalDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_horizontalDisplacement)).Returns(true);
            var result = _horizontalDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(-6));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_horizontalDisplacement)).Returns(false);
            var result = _horizontalDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(-6));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_horizontalDisplacement)).Returns(true);
            var result = _horizontalDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _horizontalDisplacement.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _horizontalDisplacement.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _horizontalDisplacement.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _horizontalDisplacement.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _horizontalDisplacement.CalculateShear();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _horizontalDisplacement.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _horizontalDisplacement.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _horizontalDisplacement.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(-100));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _horizontalDisplacement.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
