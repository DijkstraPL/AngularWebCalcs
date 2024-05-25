using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.Reactions;
using Build_IT_FrameStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class ShearResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Constructors

        public ShearResult(IFrame frame) : base(frame)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        private double _distanceFromLeftSide;

        #endregion // Public_Methods

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new ShearForce(span, _distanceFromLeftSide) { Value = 0 };

            CalculateShear(span);

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateShear(ISpan span)
        {
            CalculateShearForceFromNodeForces(span);
            CalculateShearFromContinousLoads( span);
            CalculateShearFromPointLoads( span);
        }

        private void CalculateShearForceFromNodeForces(ISpan span)
        {
            Result.Value += span.LeftForces.ShearForce;
        }

        private void CalculateShearFromContinousLoads( ISpan span)
        {
            foreach (var load in span.ContinousLoads)
                if (_distanceFromLeftSide > load.StartPosition.Position)
                    Result.Value += load.CalculateShear(_distanceFromLeftSide - load.StartPosition.Position);
        }

        private void CalculateShearFromPointLoads( ISpan span)
        {
            foreach (var load in span.PointLoads)
                if (_distanceFromLeftSide > load.Position)
                    Result.Value += load.CalculateShear();
        }

        #endregion // Private_Methods
    }
}
