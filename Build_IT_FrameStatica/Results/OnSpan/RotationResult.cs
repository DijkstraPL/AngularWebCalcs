using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class RotationResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private double _distanceFromLeftSide;

        private double _spanRotation;

        #endregion // Fields

        #region Constructors
        
        public RotationResult(IFrame frame) : base(frame)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new Rotation(span, _distanceFromLeftSide) { Value = 0 };
            
            _spanRotation = 0;

            CalculateRotation(span);

            _spanRotation *= 100;

            Result.Value += _spanRotation;

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods
        
        private void CalculateRotation(ISpan span)
        {                
                CalculateRotationFromCalculatedForcesAndDisplacements(span);
                CalculateRotationFromContinousLoads(span);
                CalculateRotationFromPointLoads(span);
        }

        private bool IsLastNode(ISpan span) =>
            span == Frame.Spans.Last(); //&& _distanceFromLeftSide == Frame.Length;

        private void CalculateRotationFromCalculatedForcesAndDisplacements(ISpan span)
        {
            _spanRotation -= span.LeftDisplacements.Rotation / 100;

            _spanRotation += span.LeftForces.ShearForce 
                * _distanceFromLeftSide
                * _distanceFromLeftSide / 2
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);

            _spanRotation += span.LeftForces.BendingMoment
                * _distanceFromLeftSide
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        private void CalculateRotationFromContinousLoads(ISpan span)
        {
            _spanRotation += span.ContinousLoads.Sum(cl => 
            cl.CalculateRotation(span, _distanceFromLeftSide, 0));
        }

        private void CalculateRotationFromPointLoads(ISpan span)
        {
            CalculateRotationFromRotationDisplacements(span);
            CalculateRotationFromShearForcesPointLoads(span);
            CalculateRotationFromBendingMomentPointLoads(span);
        }

        private void CalculateRotationFromRotationDisplacements(ISpan span)
        {
            _spanRotation += span.LeftNode.ConcentratedForces.Sum(cf
                => cf.CalculateRotationDisplacement()) / 100;
        }

        private void CalculateRotationFromShearForcesPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide <= load.Position)
                    continue;

                _spanRotation += load.CalculateShear()
                    * (_distanceFromLeftSide  - load.Position)
                    * (_distanceFromLeftSide  - load.Position) / 2
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateRotationFromBendingMomentPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide  <= load.Position)
                    continue;

                _spanRotation += load.CalculateBendingMoment(0)
                    * (_distanceFromLeftSide  - load.Position)
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }
        
        #endregion // Private_Methods
    }
}
