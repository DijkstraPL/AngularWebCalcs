using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Reactions
{
    internal class BendingMoment : Reaction
    {
        #region Constructors

        public BendingMoment() : base()
        {
        }

        public BendingMoment(ISpan span, double position) : base(span, position)
        {
        }

        #endregion // Constructors
    }
}
