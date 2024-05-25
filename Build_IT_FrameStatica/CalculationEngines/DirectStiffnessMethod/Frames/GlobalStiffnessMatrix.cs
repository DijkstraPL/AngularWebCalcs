using Build_IT_CommonTools.MatrixMath.Wrappers;
using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Frames.Interfaces;
using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Frames
{
    internal class GlobalStiffnessMatrix : IGlobalStiffnessMatrix
    {
        #region Properties

        public MatrixAdapter Matrix { get; private set; }
        public MatrixAdapter InversedMatrix => Matrix.Inverse();

        #endregion //  Properties

        #region Fields

        private readonly IFrame _frame;
        private readonly IList<(ISpan span, ISpanCalculationEngine calculationEngine)> _spanCalculationEngines;

        #endregion //  Fields

        #region Constructors

        public GlobalStiffnessMatrix(IFrame frame,
            IList<(ISpan span, ISpanCalculationEngine calculationEngine)> spanCalculationEngines)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
            _spanCalculationEngines = spanCalculationEngines ?? throw new ArgumentNullException(nameof(spanCalculationEngines));
        }

        #endregion //  Constructors

        #region Public_Methods

        public void Calculate()
        {
            if (_frame.NumberOfDegreesOfFreedom != 0)
                Matrix = MatrixAdapter.Create(_frame.NumberOfDegreesOfFreedom, _frame.NumberOfDegreesOfFreedom);

            for (int row = 0; row < _frame.NumberOfDegreesOfFreedom; row++)
                for (int col = 0; col < _frame.NumberOfDegreesOfFreedom; col++)
                    SetMatrixValues(row, col);
        }

        #endregion //  Public_Methods

        #region Private_Methods

        private void SetMatrixValues(int row, int col)
        {
            Matrix[row, col] += _spanCalculationEngines.SelectMany(s => s.calculationEngine.StiffnessMatrix.MatrixOfPositions)
                .Where(m => m.RowNumber == row && m.ColumnNumber == col).Sum(m => m.Value);
        }

        #endregion //  Private_Methods
    }
}
