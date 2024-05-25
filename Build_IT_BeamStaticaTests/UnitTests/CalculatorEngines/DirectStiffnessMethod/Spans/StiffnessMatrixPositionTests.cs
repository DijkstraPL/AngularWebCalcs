using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System;

namespace Build_IT_BeamStaticaTests.UnitTests.CalculatorEnignes.DirectStiffnessMethod.Spans
{
    [TestFixture]
    public class StiffnessMatrixPositionTests
    {
        [Test]
        public void StiffnessMatrixPositionCreationTest_Success()
        {
            var stiffnessMatrixPosition = new StiffnessMatrixPosition(10, 2 , 5);
            
            Assert.That(stiffnessMatrixPosition.Value, Is.EqualTo(10));
            Assert.That(stiffnessMatrixPosition.RowNumber, Is.EqualTo(2));
            Assert.That(stiffnessMatrixPosition.ColumnNumber, Is.EqualTo(5));
        }

        [Test]
        public void StiffnessMatrixPositionCreationTest_LessThanZeroRow_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new StiffnessMatrixPosition(5, -2, 5));
        }
    }
}
