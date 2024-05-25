using Build_IT_FrameStatica.Loads.Interfaces;
using System.Collections.Generic;

namespace Build_IT_FrameStatica.Spans.Interfaces
{
    public interface ILoadProvider
    {
        #region Properties

        ICollection<IContinousLoad> ContinousLoads { get;  }
         ICollection<ISpanLoad> PointLoads { get;  }

        #endregion // Properties
    }
}
