﻿using Build_IT_FrameStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_FrameStatica.Loads.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Loads.ContinuousLoads.UpDownTemperatureDifferenceResults
{
    internal class VerticalDeflectionResult : DisplacementResultBase
    {
        #region Constructors

        public VerticalDeflectionResult(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetValue(ISpan span, double distanceFromLeftSide, double currentLength) 
            => (ContinousLoad.StartPosition.Value - ContinousLoad.EndPosition.Value)
               / span.Section.SolidHeight
               * (distanceFromLeftSide - currentLength) / 10000
               * (distanceFromLeftSide - currentLength) / 2;

        #endregion // Public_Methods
    }
}
