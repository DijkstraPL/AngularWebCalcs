using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Spans;
using Build_IT_FrameStatica.Spans;
using Build_IT_FrameStatica.Spans.Interfaces;
using Build_IT_Data.Geometry;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_FrameStaticaTests.UnitTests.CalculatorEnignes.DirectStiffnessMethod.Spans
{
    [TestFixture]
    public class StiffnessMatrixTests
    {
        private StiffnessMatrix _stiffnessMatrix;

        private void SetUpSimpleBeam()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Section.Area).Returns(3);
            span.Setup(s => s.Material.YoungModulus).Returns(5);
            span.Setup(s => s.Section.MomentOfInteria).Returns(11);
            span.Setup(s => s.Length).Returns(7);
            span.Setup(s => s.LeftNode.HorizontalMovementNumber).Returns(0);
            span.Setup(s => s.LeftNode.VerticalMovementNumber).Returns(1);
            span.Setup(s => s.LeftNode.LeftRotationNumber).Returns(2);
            span.Setup(s => s.LeftNode.RightRotationNumber).Returns(3);
            span.Setup(s => s.RightNode.HorizontalMovementNumber).Returns(4);
            span.Setup(s => s.RightNode.VerticalMovementNumber).Returns(5);
            span.Setup(s => s.RightNode.LeftRotationNumber).Returns(6);
            span.Setup(s => s.RightNode.RightRotationNumber).Returns(7);
            span.Setup(s => s.LeftNode.Position).Returns(new Point(0, 0));
            span.Setup(s => s.RightNode.Position).Returns(new Point(7, 0));
            span.Setup(s => s.LeftForces).Returns(new Forces());
            span.Setup(s => s.RightForces).Returns(new Forces());
            span.Setup(s => s.GetLambdaX()).Returns(1);
            span.Setup(s => s.GetLambdaY()).Returns(0);

            _stiffnessMatrix = new StiffnessMatrix(span.Object);
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForLeftNodeHorizontalMovementColumn_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(matrixOfPositions[0].Value, Is.EqualTo(214.285714).Within(0.000001));
            Assert.That(matrixOfPositions[0].RowNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[0].ColumnNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[1].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[1].RowNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[1].ColumnNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[2].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[2].RowNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[2].ColumnNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[3].Value, Is.EqualTo(-214.285714).Within(0.000001));
            Assert.That(matrixOfPositions[3].RowNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[3].ColumnNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[4].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[4].RowNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[4].ColumnNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[5].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[5].RowNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[5].ColumnNumber, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForVerticalMovementColumn_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();
            
            Assert.That(matrixOfPositions[6].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[6].RowNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[6].ColumnNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[7].Value, Is.EqualTo(0.019242).Within(0.000001));
            Assert.That(matrixOfPositions[7].RowNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[7].ColumnNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[8].Value, Is.EqualTo(0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[8].RowNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[8].ColumnNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[9].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[9].RowNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[9].ColumnNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[10].Value, Is.EqualTo(-0.019242).Within(0.000001));
            Assert.That(matrixOfPositions[10].RowNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[10].ColumnNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[11].Value, Is.EqualTo(0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[11].RowNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[11].ColumnNumber, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForLeftNodeRightRotationColumn_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(matrixOfPositions[12].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[12].RowNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[12].ColumnNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[13].Value, Is.EqualTo(0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[13].RowNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[13].ColumnNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[14].Value, Is.EqualTo(0.314286).Within(0.000001));
            Assert.That(matrixOfPositions[14].RowNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[14].ColumnNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[15].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[15].RowNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[15].ColumnNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[16].Value, Is.EqualTo(-0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[16].RowNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[16].ColumnNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[17].Value, Is.EqualTo(0.157143).Within(0.000001));
            Assert.That(matrixOfPositions[17].RowNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[17].ColumnNumber, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForRightNodeHorizontalMovementColumn_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(matrixOfPositions[18].Value, Is.EqualTo(-214.285714).Within(0.000001));
            Assert.That(matrixOfPositions[18].RowNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[18].ColumnNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[19].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[19].RowNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[19].ColumnNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[20].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[20].RowNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[20].ColumnNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[21].Value, Is.EqualTo(214.285714).Within(0.000001));
            Assert.That(matrixOfPositions[21].RowNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[21].ColumnNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[22].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[22].RowNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[22].ColumnNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[23].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[23].RowNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[23].ColumnNumber, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForRightNodeVerticalMovementColumn_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(matrixOfPositions[24].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[24].RowNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[24].ColumnNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[25].Value, Is.EqualTo(-0.019242).Within(0.000001));
            Assert.That(matrixOfPositions[25].RowNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[25].ColumnNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[26].Value, Is.EqualTo(-0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[26].RowNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[26].ColumnNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[27].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[27].RowNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[27].ColumnNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[28].Value, Is.EqualTo(0.019242).Within(0.000001));
            Assert.That(matrixOfPositions[28].RowNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[28].ColumnNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[29].Value, Is.EqualTo(-0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[29].RowNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[29].ColumnNumber, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForRightNodeLeftRotationColumn_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(matrixOfPositions[30].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[30].RowNumber, Is.EqualTo(6));
            Assert.That(matrixOfPositions[30].ColumnNumber, Is.EqualTo(0));
            Assert.That(matrixOfPositions[31].Value, Is.EqualTo(0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[31].RowNumber, Is.EqualTo(6));
            Assert.That(matrixOfPositions[31].ColumnNumber, Is.EqualTo(1));
            Assert.That(matrixOfPositions[32].Value, Is.EqualTo(0.157143).Within(0.000001));
            Assert.That(matrixOfPositions[32].RowNumber, Is.EqualTo(6));
            Assert.That(matrixOfPositions[32].ColumnNumber, Is.EqualTo(3));
            Assert.That(matrixOfPositions[33].Value, Is.EqualTo(0));
            Assert.That(matrixOfPositions[33].RowNumber, Is.EqualTo(6));
            Assert.That(matrixOfPositions[33].ColumnNumber, Is.EqualTo(4));
            Assert.That(matrixOfPositions[34].Value, Is.EqualTo(-0.067347).Within(0.000001));
            Assert.That(matrixOfPositions[34].RowNumber, Is.EqualTo(6));
            Assert.That(matrixOfPositions[34].ColumnNumber, Is.EqualTo(5));
            Assert.That(matrixOfPositions[35].Value, Is.EqualTo(0.314286).Within(0.000001));
            Assert.That(matrixOfPositions[35].RowNumber, Is.EqualTo(6));
            Assert.That(matrixOfPositions[35].ColumnNumber, Is.EqualTo(6));
        }
        
        [Test]
        public void StiffnessMatrixCalculateTest_GetSize_Success()
        {
            SetUpSimpleBeam();
            _stiffnessMatrix.Calculate();

            Assert.That(_stiffnessMatrix.Size, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_GetMatrix_Success()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Section.Area).Returns(6);
            span.Setup(s => s.Material.YoungModulus).Returns(200);
            span.Setup(s => s.Section.MomentOfInteria).Returns(6000);
            span.Setup(s => s.Length).Returns(6);
            span.Setup(s => s.LeftNode.HorizontalMovementNumber).Returns(0);
            span.Setup(s => s.LeftNode.VerticalMovementNumber).Returns(1);
            span.Setup(s => s.LeftNode.LeftRotationNumber).Returns(2);
            span.Setup(s => s.LeftNode.RightRotationNumber).Returns(3);
            span.Setup(s => s.RightNode.HorizontalMovementNumber).Returns(4);
            span.Setup(s => s.RightNode.VerticalMovementNumber).Returns(5);
            span.Setup(s => s.RightNode.LeftRotationNumber).Returns(6);
            span.Setup(s => s.RightNode.RightRotationNumber).Returns(7);
            span.Setup(s => s.LeftNode.Position).Returns(new Point(0,0));
            span.Setup(s => s.RightNode.Position).Returns(new Point(0, -6));
            span.Setup(s => s.LeftForces).Returns(new Forces());
            span.Setup(s => s.RightForces).Returns(new Forces());
            span.Setup(s => s.GetLambdaX()).Returns(0);
            span.Setup(s => s.GetLambdaY()).Returns(-1);

            var stiffnessMatrix = new StiffnessMatrix(span.Object);

            stiffnessMatrix.Calculate();
            var matrixOfPositions = stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(stiffnessMatrix.Matrix.RowCount, Is.EqualTo(6));
            Assert.That(stiffnessMatrix.Matrix.ColumnCount, Is.EqualTo(6));

            Assert.That(stiffnessMatrix.Matrix[0,0], Is.EqualTo(666.667).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[0,1], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[0,2], Is.EqualTo(2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[0,3], Is.EqualTo(-666.667).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[0,4], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[0,5], Is.EqualTo(2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[1,0], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[1,1], Is.EqualTo(20000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[1,2], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[1,3], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[1,4], Is.EqualTo(-20000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[1,5], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[2,0], Is.EqualTo(2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[2,1], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[2,2], Is.EqualTo(8000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[2,3], Is.EqualTo(-2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[2,4], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[2,5], Is.EqualTo(4000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[3,0], Is.EqualTo(-666.667).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[3,1], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[3,2], Is.EqualTo(-2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[3,3], Is.EqualTo(666.667).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[3,4], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[3,5], Is.EqualTo(-2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[4,0], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[4,1], Is.EqualTo(-20000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[4,2], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[4,3], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[4,4], Is.EqualTo(20000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[4,5], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[5,0], Is.EqualTo(2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[5,1], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[5,2], Is.EqualTo(4000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[5,3], Is.EqualTo(-2000).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[5,4], Is.EqualTo(0).Within(0.001));
            Assert.That(stiffnessMatrix.Matrix[5,5], Is.EqualTo(8000).Within(0.001));
        }
    }
}
