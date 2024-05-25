﻿using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.BendingMomentLoadResults
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
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;

            return ContinousLoad.StartPosition.Value
                * distanceFromTheClosestLeftNode
                * distanceFromTheClosestLeftNode / 2
                * distanceFromTheClosestLeftNode / 3
               / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        #endregion // Public_Methods
    }
}
