using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces
{
    public interface IStiffnessMatrixPosition
    {
        #region Properties

        short RowNumber { get; }
        short ColumnNumber { get; }
        double Value { get; set; }

        #endregion // Properties
    }
}
