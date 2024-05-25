using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_FrameStatica.Results.Interfaces
{
    public interface IGetResult
    {
        #region Properties

        ICollection<IResultValue> Values { get; }

        #endregion // Properties

        #region Public_Methods

        void SetValues();
        IResultValue GetValue(double distanceFromLeftSide, short spanNumber);
        IResultValue GetMaxValue();
        IResultValue GetMaxValue(short[] spanNumbers, double startPosition = 0, double? endPosition = null);
        IResultValue GetMinValue();
        IResultValue GetMinValue(short[] spanNumbers, double startPosition = 0, double? endPosition = null);

        #endregion // Public_Methods
    }
}
