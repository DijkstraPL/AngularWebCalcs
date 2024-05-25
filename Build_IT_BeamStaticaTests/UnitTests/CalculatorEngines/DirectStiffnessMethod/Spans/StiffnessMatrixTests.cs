using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Spans;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_BeamStaticaTests.UnitTests.CalculatorEnignes.DirectStiffnessMethod.Spans
{
    [TestFixture]
    public class StiffnessMatrixTests
    {
        private StiffnessMatrix _stiffnessMatrix;

        [SetUp]
        public void SetUp()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Section).Returns(new SectionData { Area = 3, MomentOfInteria = 11 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 5 });
            span.Setup(s => s.Length).Returns(7);
            span.Setup(s => s.LeftNode.HorizontalMovementNumber).Returns(0);
            span.Setup(s => s.LeftNode.VerticalMovementNumber).Returns(1);
            span.Setup(s => s.LeftNode.LeftRotationNumber).Returns(2);
            span.Setup(s => s.LeftNode.RightRotationNumber).Returns(3);
            span.Setup(s => s.RightNode.HorizontalMovementNumber).Returns(4);
            span.Setup(s => s.RightNode.VerticalMovementNumber).Returns(5);
            span.Setup(s => s.RightNode.LeftRotationNumber).Returns(6);
            span.Setup(s => s.RightNode.RightRotationNumber).Returns(7);

            _stiffnessMatrix = new StiffnessMatrix(span.Object);
        }

        [Test]
        public void StiffnessMatrixCalculateTest_MatrixOfPositionsForLeftNodeHorizontalMovementColumn_Success()
        {
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
            _stiffnessMatrix.Calculate();

            Assert.That(_stiffnessMatrix.Size, Is.EqualTo(6));
        }

        [Test]
        public void StiffnessMatrixCalculateTest_GetMatrix_Success()
        {
            _stiffnessMatrix.Calculate();
            var matrixOfPositions = _stiffnessMatrix.MatrixOfPositions.ToList();

            Assert.That(_stiffnessMatrix.Matrix.RowCount, Is.EqualTo(6));
            Assert.That(_stiffnessMatrix.Matrix.ColumnCount, Is.EqualTo(6));

            Assert.That(_stiffnessMatrix.Matrix[0,0], Is.EqualTo(matrixOfPositions[0].Value));
            Assert.That(_stiffnessMatrix.Matrix[0,1], Is.EqualTo(matrixOfPositions[1].Value));
            Assert.That(_stiffnessMatrix.Matrix[0,2], Is.EqualTo(matrixOfPositions[2].Value));
            Assert.That(_stiffnessMatrix.Matrix[0,3], Is.EqualTo(matrixOfPositions[3].Value));
            Assert.That(_stiffnessMatrix.Matrix[0,4], Is.EqualTo(matrixOfPositions[4].Value));
            Assert.That(_stiffnessMatrix.Matrix[0,5], Is.EqualTo(matrixOfPositions[5].Value));
            Assert.That(_stiffnessMatrix.Matrix[1,0], Is.EqualTo(matrixOfPositions[6].Value));
            Assert.That(_stiffnessMatrix.Matrix[1,1], Is.EqualTo(matrixOfPositions[7].Value));
            Assert.That(_stiffnessMatrix.Matrix[1,2], Is.EqualTo(matrixOfPositions[8].Value));
            Assert.That(_stiffnessMatrix.Matrix[1,3], Is.EqualTo(matrixOfPositions[9].Value));
            Assert.That(_stiffnessMatrix.Matrix[1,4], Is.EqualTo(matrixOfPositions[10].Value));
            Assert.That(_stiffnessMatrix.Matrix[1,5], Is.EqualTo(matrixOfPositions[11].Value));
            Assert.That(_stiffnessMatrix.Matrix[2,0], Is.EqualTo(matrixOfPositions[12].Value));
            Assert.That(_stiffnessMatrix.Matrix[2,1], Is.EqualTo(matrixOfPositions[13].Value));
            Assert.That(_stiffnessMatrix.Matrix[2,2], Is.EqualTo(matrixOfPositions[14].Value));
            Assert.That(_stiffnessMatrix.Matrix[2,3], Is.EqualTo(matrixOfPositions[15].Value));
            Assert.That(_stiffnessMatrix.Matrix[2,4], Is.EqualTo(matrixOfPositions[16].Value));
            Assert.That(_stiffnessMatrix.Matrix[2,5], Is.EqualTo(matrixOfPositions[17].Value));
            Assert.That(_stiffnessMatrix.Matrix[3,0], Is.EqualTo(matrixOfPositions[18].Value));
            Assert.That(_stiffnessMatrix.Matrix[3,1], Is.EqualTo(matrixOfPositions[19].Value));
            Assert.That(_stiffnessMatrix.Matrix[3,2], Is.EqualTo(matrixOfPositions[20].Value));
            Assert.That(_stiffnessMatrix.Matrix[3,3], Is.EqualTo(matrixOfPositions[21].Value));
            Assert.That(_stiffnessMatrix.Matrix[3,4], Is.EqualTo(matrixOfPositions[22].Value));
            Assert.That(_stiffnessMatrix.Matrix[3,5], Is.EqualTo(matrixOfPositions[23].Value));
            Assert.That(_stiffnessMatrix.Matrix[4,0], Is.EqualTo(matrixOfPositions[24].Value));
            Assert.That(_stiffnessMatrix.Matrix[4,1], Is.EqualTo(matrixOfPositions[25].Value));
            Assert.That(_stiffnessMatrix.Matrix[4,2], Is.EqualTo(matrixOfPositions[26].Value));
            Assert.That(_stiffnessMatrix.Matrix[4,3], Is.EqualTo(matrixOfPositions[27].Value));
            Assert.That(_stiffnessMatrix.Matrix[4,4], Is.EqualTo(matrixOfPositions[28].Value));
            Assert.That(_stiffnessMatrix.Matrix[4,5], Is.EqualTo(matrixOfPositions[29].Value));
            Assert.That(_stiffnessMatrix.Matrix[5,0], Is.EqualTo(matrixOfPositions[30].Value));
            Assert.That(_stiffnessMatrix.Matrix[5,1], Is.EqualTo(matrixOfPositions[31].Value));
            Assert.That(_stiffnessMatrix.Matrix[5,2], Is.EqualTo(matrixOfPositions[32].Value));
            Assert.That(_stiffnessMatrix.Matrix[5,3], Is.EqualTo(matrixOfPositions[33].Value));
            Assert.That(_stiffnessMatrix.Matrix[5,4], Is.EqualTo(matrixOfPositions[34].Value));
            Assert.That(_stiffnessMatrix.Matrix[5,5], Is.EqualTo(matrixOfPositions[35].Value));
        }
    }
}
