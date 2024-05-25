using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Reactions
{
    internal class NormalForce : Reaction
    {
        #region Constructors

        public NormalForce() : base()
        {
        }

        public NormalForce(ISpan span, double position) : base(span, position)
        {
        }

        #endregion
    }
}
