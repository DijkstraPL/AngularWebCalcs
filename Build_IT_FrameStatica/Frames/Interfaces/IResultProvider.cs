using Build_IT_FrameStatica.Results.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_FrameStatica.Frames.Interfaces
{
    public interface IResultProvider
    {
        #region Properties

        IResultsContainer Results { get; }

        #endregion // Properties
    }
}
