using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class HorizontalDeflectionResult : Result
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

        public HorizontalDeflectionResult(IFrame frame) : base(frame)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new Rotation(span, distanceFromLeftSide) { Value = 0 };

            _spanDeflection = 0;

            CalculateDeflection(span);

            _spanDeflection *= 10;
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
        
        private void CalculateDeflectionFromCalculatedForcesAndDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftDisplacements.NormalDeflection / 10;

            _spanDeflection -= span.LeftForces.NormalForce * _distanceFromLeftSide 
                / (span.Material.YoungModulus * span.Section.Area);
        }
        
        private void CalculateDeflectionFromContinousLoads(ISpan span)
        {
            _spanDeflection -= span.ContinousLoads.Sum(cl =>
            cl.CalculateHorizontalDeflection(span, _distanceFromLeftSide, 0));
        }

        private void CalculateDeflectionFromPointLoads(ISpan span)
        {
            CalculateDeflectionFromHorizontalDisplacements(span);
            CalculateDeflectionFromNormalForcesPointLoads(span);
        }

        private void CalculateDeflectionFromHorizontalDisplacements(ISpan span)
        {
            _spanDeflection -= span.LeftNode.ConcentratedForces.Sum(cf
                => cf.CalculateHorizontalDisplacement()) / 10;
        }

        private void CalculateDeflectionFromNormalForcesPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide <= load.Position)
                    continue;

                _spanDeflection -= load.CalculateNormalForce()
                    * (_distanceFromLeftSide - load.Position)
                    / (span.Material.YoungModulus * span.Section.Area);
            }
        }
        
        #endregion // Private_Methods
    }
}
