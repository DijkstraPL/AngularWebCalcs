using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.Reactions;
using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class NormalForceResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private double _distanceFromLeftSide;

        #endregion // Fields

        #region Constructors

        public NormalForceResult(IFrame frame) : base(frame)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new NormalForce(span, _distanceFromLeftSide) { Value = 0 };

            CalculateNormalForce(span);

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateNormalForce(ISpan span)
        {
            CalculateNormalForceFromNodeForces(span);
            CalculateNormalForceFromContinousLoads(span);
            CalculateNormalForceFromPointLoads(span);
        }

        private void CalculateNormalForceFromNodeForces(ISpan span)
        {
            Result.Value += span.LeftForces.NormalForce;
        }

        private void CalculateNormalForceFromContinousLoads(ISpan span)
        {
            foreach (var load in span.ContinousLoads)
                if (_distanceFromLeftSide > load.StartPosition.Position)
                    Result.Value += load.CalculateNormalForce(_distanceFromLeftSide - load.StartPosition.Position);
        }

        private void CalculateNormalForceFromPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
                if (_distanceFromLeftSide > load.Position)
                    Result.Value += load.CalculateNormalForce();
        }

        #endregion // Private_Methods
    }
}
