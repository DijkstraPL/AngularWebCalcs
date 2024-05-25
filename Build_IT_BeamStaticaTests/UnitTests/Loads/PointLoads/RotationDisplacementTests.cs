using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.PointLoads
{
    [TestFixture]
    public class RotationDisplacementTests
    {
        private ISpanLoad _rotationDisplacement;
        private Mock<ISpan> _span;

        [SetUp]
        public void SetUpData()
        {
            _span = new Mock<ISpan>();
            _span.Setup(s => s.Length).Returns(10);
            _span.Setup(s => s.Section)
                .Returns(new SectionData { MomentOfInteria = 2 });
            _span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus=3 }) ;

            _rotationDisplacement = new RotationDisplacement(value: -10);
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(false);
            var result = _rotationDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(-0.000628319).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_LeftNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(true);
            var result = _rotationDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0.000628319).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_NodeNotContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(false);
            var result = _rotationDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0.000628319).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorShearMemberTest_RightNode_NodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(true);
            var result = _rotationDisplacement.CalculateSpanLoadVectorShearMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(-0.000628319).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_LeftNodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(true);
            _span.Setup(s => s.RightNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(false);
            var result = _rotationDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0.00418879).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_LeftNode_RightNodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(false);
            _span.Setup(s => s.RightNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(true);
            var result = _rotationDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0.002094395).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_LeftNodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(true);
            _span.Setup(s => s.RightNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(false);
            var result = _rotationDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0.002094395).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorBendingMomentMemberTest_RightNode_RightNodeContainsDisplacement_Success()
        {
            _span.Setup(s => s.LeftNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(false);
            _span.Setup(s => s.RightNode.ConcentratedForces.Contains(_rotationDisplacement)).Returns(true);
            var result = _rotationDisplacement.CalculateSpanLoadBendingMomentMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0.00418879).Within(0.000000001));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_LeftNode_Success()
        {
            var result = _rotationDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: true);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateSpanLoadVectorNormalForceMemberTest_RightNode_Success()
        {
            var result = _rotationDisplacement.CalculateSpanLoadVectorNormalForceMember(_span.Object, leftNode: false);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceJointMemberTest_Success()
        {
            var result = _rotationDisplacement.CalculateJointLoadVectorNormalForceMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearForceJointMemberTest_Success()
        {
            var result = _rotationDisplacement.CalculateJointLoadVectorShearMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentJointMemberTest_Success()
        {
            var result = _rotationDisplacement.CalculateJointLoadVectorBendingMomentMember();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateNormalForceTest_Success()
        {
            var result = _rotationDisplacement.CalculateNormalForce();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateShearTest_Success()
        {
            var result = _rotationDisplacement.CalculateShear();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateBendingMomentTest_Success()
        {
            var result = _rotationDisplacement.CalculateBendingMoment(distanceFromLoad: 5);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateRotationTest_Success()
        {
            var result = _rotationDisplacement.CalculateRotationDisplacement();

            Assert.That(result, Is.EqualTo(0.174532925).Within(0.000000001));
        }

        [Test]
        public void CalculateHorizontalDeflectionTest_Success()
        {
            var result = _rotationDisplacement.CalculateHorizontalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVerticalDeflectionTest_Success()
        {
            var result = _rotationDisplacement.CalculateVerticalDisplacement();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
