using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class VerticalDisplacementTests
    {
        private ISpanLoad _verticalDisplacement;
        private Mock<ISpan> _span;

        [SetUp]
        public void SetUpData()
        {
            _span = new Mock<ISpan>();
            _span.Setup(s => s.Length).Returns(10);
            _span.Setup(s => s.Section)
                .Returns(new SectionData { MomentOfInteria = 3 });
            _span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });
            
            _verticalDisplacement = new VerticalDisplacement(value: -100);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(false);
            var result = _verticalDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0.000072).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(true);
            var result = _verticalDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(-0.000072).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(false);
            var result = _verticalDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(-0.000072).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(true);
            var result = _verticalDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0.000072).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(false);
            var result = _verticalDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0.00036).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(true);
            var result = _verticalDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(-0.00036).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(false);
            var result = _verticalDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0.00036).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_verticalDisplacement)).Returns(true);
            var result = _verticalDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(-0.00036).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _verticalDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _verticalDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _verticalDisplacement.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _verticalDisplacement.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _verticalDisplacement.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _verticalDisplacement.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _verticalDisplacement.CalculateShear();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _verticalDisplacement.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _verticalDisplacement.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _verticalDisplacement.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _verticalDisplacement.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(-100));
        }
    }
}
