using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Reactions
{
    internal class ShearForce : Reaction
    {
        #region Constructors

        public ShearForce() : base()
        {
        }

        public ShearForce(ISpan span, double position) : base(span, position)
        {
        }

        #endregion // Constructors
    }
}
