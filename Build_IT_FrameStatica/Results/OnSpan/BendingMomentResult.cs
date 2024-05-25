using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class BendingMomentResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields
        
        private double _distanceFromLeftSide;

        #endregion // Fields

        #region Constructors

        public BendingMomentResult(IFrame frame) : base(frame)
        {
        }

        #endregion //Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new Reactions.BendingMoment(span, _distanceFromLeftSide) { Value = 0 };
            CalculateBendingMoment(span);

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateBendingMoment(ISpan span)
        {
            CalculateBendingMomentFromNodeForces( span);
            CalculateBendingMomentFromContinousLoads( span);
            CalculateBendingMomentFromPointLoads( span);
        }

        private void CalculateBendingMomentFromNodeForces(ISpan span)
        {
            Result.Value += span.LeftForces.BendingMoment;
            Result.Value += span.LeftForces.ShearForce * _distanceFromLeftSide;
        }

        private void CalculateBendingMomentFromContinousLoads( ISpan span)
        {
            foreach (var load in span.ContinousLoads)
                if (_distanceFromLeftSide > load.StartPosition.Position)
                    Result.Value += load.CalculateBendingMoment(_distanceFromLeftSide - load.StartPosition.Position);
        }

        private void CalculateBendingMomentFromPointLoads( ISpan span)
        {
            foreach (var load in span.PointLoads)
                if (_distanceFromLeftSide > load.Position)
                    Result.Value += load.CalculateBendingMoment(_distanceFromLeftSide - load.Position);
        }

        #endregion // Private_Methods
    }
}
