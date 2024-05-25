﻿using Build_IT_FrameStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_FrameStatica.Loads.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Loads.ContinuousLoads.BendingMomentLoadResults
{
    internal class RotationResult : DisplacementResultBase
    {
        #region Constructors

        public RotationResult(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetValue(ISpan span, double distanceFromLeftSide, double currentLength)
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;

            return ContinousLoad.StartPosition.Value 
                * distanceFromTheClosestLeftNode / 2 
                * distanceFromTheClosestLeftNode
               / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        #endregion // Public_Methods
    }
}
