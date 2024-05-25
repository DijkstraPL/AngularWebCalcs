using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Build_IT_CommonTools.MatrixMath.Wrappers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.UnitTests.CalculatorEngines.DirectStiffnessMethod.Spans
{
    [TestFixture]
    public class SpanCalculationEngineTests
    {
        [Test]
        public void SpanCalculationEngine_Creation_Success()
        {
            var span = new Mock<ISpan>();
            var stiffnessMatrix = new Mock<IStiffnessMatrix>();

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, stiffnessMatrix.Object);

            Assert.That(spanCalculationEngine.StiffnessMatrix, Is.SameAs(stiffnessMatrix.Object));
        }

        [Test]
        public void SpanCalculationEngine_Creation_NullStiffnessMatrix_Success()
        {
            var span = new Mock<ISpan>();

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, null);

            Assert.That(spanCalculationEngine.StiffnessMatrix, Is.Not.Null);
        }

        [Test]
        public void SpanCalculationEngine_Creation_NullSpan_Success()
        {
            var stiffnessMatrix = new Mock<IStiffnessMatrix>();

            Assert.Throws<ArgumentNullException>(() => new SpanCalculationEngine(null, stiffnessMatrix.Object));
        }

        [Test]
        public void Calculate_SpanLoadVector_NodeLoadsNotIncluded_Success()
        {
            Mock<IStiffnessMatrix> stiffnessMatrix = new Mock<IStiffnessMatrix>();
            stiffnessMatrix.Setup(sm => sm.Size).Returns(6);
            Mock<ISpan> span = SetFakeObjects(includeNoadLoads: false);

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, stiffnessMatrix.Object);

            spanCalculationEngine.CalculateSpanLoadVector();

            Assert.That(spanCalculationEngine.LoadVector[0], Is.EqualTo(6));
            Assert.That(spanCalculationEngine.LoadVector[1], Is.EqualTo(3));
            Assert.That(spanCalculationEngine.LoadVector[2], Is.EqualTo(10));
            Assert.That(spanCalculationEngine.LoadVector[3], Is.EqualTo(2));
            Assert.That(spanCalculationEngine.LoadVector[4], Is.EqualTo(4));
            Assert.That(spanCalculationEngine.LoadVector[5], Is.EqualTo(6));
        }

        [Test]
        public void Calculate_SpanLoadVector_NodeLoadsIncluded_Success()
        {
            Mock<IStiffnessMatrix> stiffnessMatrix = new Mock<IStiffnessMatrix>();
            stiffnessMatrix.Setup(sm => sm.Size).Returns(6);
            Mock<ISpan> span = SetFakeObjects(includeNoadLoads: true);

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, stiffnessMatrix.Object);

            spanCalculationEngine.CalculateSpanLoadVector();

            Assert.That(spanCalculationEngine.LoadVector[0], Is.EqualTo(28));
            Assert.That(spanCalculationEngine.LoadVector[1], Is.EqualTo(29));
            Assert.That(spanCalculationEngine.LoadVector[2], Is.EqualTo(44));
            Assert.That(spanCalculationEngine.LoadVector[3], Is.EqualTo(4));
            Assert.That(spanCalculationEngine.LoadVector[4], Is.EqualTo(8));
            Assert.That(spanCalculationEngine.LoadVector[5], Is.EqualTo(12));
        }

        private static Mock<ISpan> SetFakeObjects(bool includeNoadLoads)
        {
            var leftNode = new Mock<INode>();
            var rightNode = new Mock<INode>();

            Mock<ISpan> span = new Mock<ISpan>();
            span.Setup(s => s.IncludeSelfWeight).Returns(false);
            span.Setup(s => s.LeftNode).Returns(leftNode.Object);
            span.Setup(s => s.RightNode).Returns(rightNode.Object);

            var continousLoad = new Mock<IContinousLoad>();
            continousLoad.Setup(cl => cl.CalculateSpanLoadVectorNormalForceMember(span.Object, true)).Returns(5);
            continousLoad.Setup(cl => cl.CalculateSpanLoadVectorShearMember(span.Object, true)).Returns(1);
            continousLoad.Setup(cl => cl.CalculateSpanLoadVectorBendingMomentMember(span.Object, true)).Returns(3);
            continousLoad.Setup(cl => cl.CalculateSpanLoadVectorNormalForceMember(span.Object, false)).Returns(1);
            continousLoad.Setup(cl => cl.CalculateSpanLoadVectorShearMember(span.Object, false)).Returns(2);
            continousLoad.Setup(cl => cl.CalculateSpanLoadVectorBendingMomentMember(span.Object, false)).Returns(3);
            var continousLoads = new List<IContinousLoad>() { continousLoad.Object };

            var pointLoad = new Mock<ISpanLoad>();
            pointLoad.Setup(pl => pl.CalculateSpanLoadVectorNormalForceMember(span.Object, true)).Returns(1);
            pointLoad.Setup(pl => pl.CalculateSpanLoadVectorShearMember(span.Object, true)).Returns(2);
            pointLoad.Setup(pl => pl.CalculateSpanLoadBendingMomentMember(span.Object, true)).Returns(7);
            pointLoad.Setup(pl => pl.CalculateSpanLoadVectorNormalForceMember(span.Object, false)).Returns(1);
            pointLoad.Setup(pl => pl.CalculateSpanLoadVectorShearMember(span.Object, false)).Returns(2);
            pointLoad.Setup(pl => pl.CalculateSpanLoadBendingMomentMember(span.Object, false)).Returns(3);
            var pointLoads = new List<ISpanLoad>() { pointLoad.Object };

            var nodeLoad = new Mock<ISpanLoad>();
            nodeLoad.Setup(nl => nl.IncludeInSpanLoadCalculations).Returns(includeNoadLoads);
            if (includeNoadLoads)
            {
                nodeLoad.Setup(nl => nl.CalculateSpanLoadVectorNormalForceMember(span.Object, true)).Returns(11);
                nodeLoad.Setup(nl => nl.CalculateSpanLoadVectorShearMember(span.Object, true)).Returns(13);
                nodeLoad.Setup(nl => nl.CalculateSpanLoadBendingMomentMember(span.Object, true)).Returns(17);
                nodeLoad.Setup(nl => nl.CalculateSpanLoadVectorNormalForceMember(span.Object, false)).Returns(1);
                nodeLoad.Setup(nl => nl.CalculateSpanLoadVectorShearMember(span.Object, false)).Returns(2);
                nodeLoad.Setup(nl => nl.CalculateSpanLoadBendingMomentMember(span.Object, false)).Returns(3);
            }
            var nodeLoads = new List<INodeLoad>() { nodeLoad.Object };

            leftNode.Setup(ln => ln.ConcentratedForces).Returns(nodeLoads);
            rightNode.Setup(ln => ln.ConcentratedForces).Returns(nodeLoads);

            span.Setup(s => s.ContinousLoads).Returns(continousLoads);
            span.Setup(s => s.PointLoads).Returns(pointLoads);

            return span;
        }

        [Test]
        public void Calculate_DisplacementVector_LargeNumberOfDegreesOfFreedom_Success()
        {
            Mock<IStiffnessMatrix> stiffnessMatrix = new Mock<IStiffnessMatrix>();
            stiffnessMatrix.Setup(sm => sm.Size).Returns(6);
            Mock<ISpan> span = new Mock<ISpan>();
            SetUpSpan(span);

            VectorAdapter deflectionVector = SetUpDeflectionVector();

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, stiffnessMatrix.Object);
            spanCalculationEngine.CalculateDisplacement(deflectionVector, numberOfDegreesOfFreedom: 8);

            Assert.That(spanCalculationEngine.Displacements[0], Is.EqualTo(2));
            Assert.That(spanCalculationEngine.Displacements[1], Is.EqualTo(3));
            Assert.That(spanCalculationEngine.Displacements[2], Is.EqualTo(4));
            Assert.That(spanCalculationEngine.Displacements[3], Is.EqualTo(5));
            Assert.That(spanCalculationEngine.Displacements[4], Is.EqualTo(6));
            Assert.That(spanCalculationEngine.Displacements[5], Is.EqualTo(7));

            Assert.That(span.Object.LeftNode.HorizontalDeflection.Value, Is.EqualTo(2000));
            Assert.That(span.Object.LeftNode.VerticalDeflection.Value, Is.EqualTo(3000));
            Assert.That(span.Object.LeftNode.RightRotation.Value, Is.EqualTo(4));
            Assert.That(span.Object.RightNode.HorizontalDeflection.Value, Is.EqualTo(5000));
            Assert.That(span.Object.RightNode.VerticalDeflection.Value, Is.EqualTo(6000));
            Assert.That(span.Object.RightNode.LeftRotation.Value, Is.EqualTo(7));

            Assert.That(span.Object.LeftNode.LeftRotation, Is.Null);
            Assert.That(span.Object.RightNode.RightRotation, Is.Null);
        }

        [Test]
        public void Calculate_DisplacementVector_SmallNumberOfDegreesOfFreedom_Success()
        {
            Mock<IStiffnessMatrix> stiffnessMatrix = new Mock<IStiffnessMatrix>();
            stiffnessMatrix.Setup(sm => sm.Size).Returns(6);
            Mock<ISpan> span = new Mock<ISpan>();
            SetUpSpan(span);

            VectorAdapter deflectionVector = SetUpDeflectionVector();

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, stiffnessMatrix.Object);
            spanCalculationEngine.CalculateDisplacement(deflectionVector, numberOfDegreesOfFreedom: 0);

            Assert.That(spanCalculationEngine.Displacements[0], Is.EqualTo(0));
            Assert.That(spanCalculationEngine.Displacements[1], Is.EqualTo(0));
            Assert.That(spanCalculationEngine.Displacements[2], Is.EqualTo(0));
            Assert.That(spanCalculationEngine.Displacements[3], Is.EqualTo(0));
            Assert.That(spanCalculationEngine.Displacements[4], Is.EqualTo(0));
            Assert.That(spanCalculationEngine.Displacements[5], Is.EqualTo(0));

            Assert.That(span.Object.LeftNode.HorizontalDeflection.Value, Is.EqualTo(0));
            Assert.That(span.Object.LeftNode.VerticalDeflection.Value, Is.EqualTo(0));
            Assert.That(span.Object.LeftNode.RightRotation.Value, Is.EqualTo(0));
            Assert.That(span.Object.RightNode.HorizontalDeflection.Value, Is.EqualTo(0));
            Assert.That(span.Object.RightNode.VerticalDeflection.Value, Is.EqualTo(0));
            Assert.That(span.Object.RightNode.LeftRotation.Value, Is.EqualTo(0));

            Assert.That(span.Object.LeftNode.LeftRotation, Is.Null);
            Assert.That(span.Object.RightNode.RightRotation, Is.Null);
        }

        private static VectorAdapter SetUpDeflectionVector()
        {
            var deflectionVector = VectorAdapter.Create(8);
            deflectionVector[0] = 1;
            deflectionVector[1] = 2;
            deflectionVector[2] = 3;
            deflectionVector[3] = 4;
            deflectionVector[4] = 5;
            deflectionVector[5] = 6;
            deflectionVector[6] = 7;
            deflectionVector[7] = 8;
            return deflectionVector;
        }

        private static void SetUpSpan(Mock<ISpan> span)
        {
            span.Setup(s => s.LeftNode.HorizontalMovementNumber).Returns(1);
            span.Setup(s => s.LeftNode.VerticalMovementNumber).Returns(2);
            span.Setup(s => s.LeftNode.RightRotationNumber).Returns(3);
            span.Setup(s => s.RightNode.HorizontalMovementNumber).Returns(4);
            span.Setup(s => s.RightNode.VerticalMovementNumber).Returns(5);
            span.Setup(s => s.RightNode.LeftRotationNumber).Returns(6);

            span.SetupProperty(s => s.LeftNode.HorizontalDeflection.Value);
            span.SetupProperty(s => s.LeftNode.VerticalDeflection.Value);
            span.SetupProperty(s => s.LeftNode.RightRotation.Value);
            span.SetupProperty(s => s.RightNode.HorizontalDeflection.Value);
            span.SetupProperty(s => s.RightNode.VerticalDeflection.Value);
            span.SetupProperty(s => s.RightNode.LeftRotation.Value);
        }

        [Test]
        public void SpanCalculate_Force_Success()
        {
            Mock<IStiffnessMatrix> stiffnessMatrix = new Mock<IStiffnessMatrix>();
            stiffnessMatrix.Setup(sm => sm.Matrix)
                .Returns(MatrixAdapter.Create(rows: 2, columns: 2, values: new double[4] { 1, 2, 3, 4 }));

            Mock<ISpan> span = new Mock<ISpan>();

            var spanCalculationEngine = new SpanCalculationEngine(span.Object, stiffnessMatrix.Object);
            var loadVector = VectorAdapter.Create(values: new double[2] { 2, 1 });
            var displacements = VectorAdapter.Create(values: new double[2] { 1, 3 });

            spanCalculationEngine.CalculateForce(loadVector, displacements);

            Assert.That(spanCalculationEngine.Forces[0], Is.EqualTo(12));
            Assert.That(spanCalculationEngine.Forces[1], Is.EqualTo(15));
        }

    }
}
