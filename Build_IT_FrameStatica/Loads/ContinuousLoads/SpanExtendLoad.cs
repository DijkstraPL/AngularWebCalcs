using Build_IT_Data.Materials.Intefaces;
using Build_IT_FrameStatica.Loads.ContinuousLoads.SpanExtendLoadResult;
using Build_IT_FrameStatica.Loads.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Loads.ContinuousLoads
{
    internal class SpanExtendLoad : ContinuousLoad
    {
        #region Factories

        public static IContinousLoad Create(ISpan span, double lengthIncrease)
        {
            return new SpanExtendLoad(
                           new LoadData(0, 0),
                           new LoadData(span.Length, lengthIncrease));
        }

        #endregion // Factories

        #region Constructors

        private SpanExtendLoad(
            ILoadWithPosition startPosition, ILoadWithPosition endPosition)
            : base(startPosition, endPosition)
        {
            HorizontalDeflectionResult = new HorizontalDeflectionResult(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? 1.0 : -1.0;
            return sign * (this.EndPosition.Value - this.StartPosition.Value) / span.Length
               * span.Section.Area * span.Material.YoungModulus / 10;
        }

        #endregion // Public_Methods
    }
}
