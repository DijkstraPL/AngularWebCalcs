using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Loads.Interfaces;
using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class VerticalDeflectionResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private const double _nextToNodePosition = 0.00000001;

        private double _distanceFromLeftSide;

        private double _spanDeflection;

        #endregion // Fields

        #region Constructors

        public VerticalDeflectionResult(IFrame frame) : base(frame)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new Rotation(span,distanceFromLeftSide) { Value = 0 };

            _spanDeflection = 0;

            CalculateDeflection(span);

            _spanDeflection *= 100000;
            Result.Value += _spanDeflection;

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateDeflection(ISpan span)
        {
            CalculateDeflectionFromCalculatedForcesAndDisplacements(span);
            CalculateDeflectionFromContinousLoads(span);
            CalculateDeflectionFromPointLoads(span);
        }

        private bool IsLastNode(ISpan span) =>
            span == Frame.Spans.Last(); //&& _distanceFromLeftSide == Frame.Length;

        private void CalculateDeflectionFromCalculatedForcesAndDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftDisplacements.ShearDeflection / 100000;
            _spanDeflection -= span.LeftDisplacements.Rotation * _distanceFromLeftSide / 100;
            
            _spanDeflection += span.LeftForces.ShearForce
                * _distanceFromLeftSide
                * _distanceFromLeftSide / 2
                * _distanceFromLeftSide / 3
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);

            _spanDeflection += span.LeftForces.BendingMoment
                * _distanceFromLeftSide
                * _distanceFromLeftSide / 2
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        private void CalculateDeflectionFromContinousLoads(ISpan span)
        {
            _spanDeflection += span.ContinousLoads.Sum(cl =>
            cl.CalculateVerticalDeflection(span, _distanceFromLeftSide, 0));
        }

        private void CalculateDeflectionFromPointLoads(ISpan span)
        {
            CalculateRotationFromRotationDisplacements(span);
            CalculateDeflectionFromVerticalDisplacements(span);
            CalculateDeflectionFromShearForcesPointLoads(span);
            CalculateDeflectionFromBendingMomentPointLoads(span);
        }

        private void CalculateDeflectionFromShearForcesPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide <= load.Position)
                    continue;

                _spanDeflection += load.CalculateShear()
                    * (_distanceFromLeftSide - load.Position)
                    * (_distanceFromLeftSide - load.Position) / 2
                    * (_distanceFromLeftSide - load.Position) / 3
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateDeflectionFromBendingMomentPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide <= load.Position)
                    continue;

                _spanDeflection += load.CalculateBendingMoment(0)
                    * (_distanceFromLeftSide - load.Position)
                    * (_distanceFromLeftSide - load.Position) / 2
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }   

        private void CalculateRotationFromRotationDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftNode.ConcentratedForces.Sum(cf
                => cf.CalculateRotationDisplacement()) * _distanceFromLeftSide  / 100;
        }

        private void CalculateDeflectionFromVerticalDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftNode.ConcentratedForces.Sum(cf
                => cf.CalculateVerticalDisplacement()) / 100000;
        }
    }

    #endregion // Private_Methods
}
