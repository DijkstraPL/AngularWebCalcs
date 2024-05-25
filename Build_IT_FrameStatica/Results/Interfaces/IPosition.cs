using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_FrameStatica.Results.Interfaces
{
    public interface IPosition
    {
        #region Properties
        
        ISpan Span { get; }
        double Position { get; }

        #endregion // Properties
    }
}
