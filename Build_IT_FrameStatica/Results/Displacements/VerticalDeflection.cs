using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Displacements
{
    internal class VerticalDeflection : Displacement
    {
        #region Constructors

        public VerticalDeflection() : base()
        {
        }

        public VerticalDeflection(ISpan span, double position) : base(span, position)
        {
        }

        #endregion // Constructors
    }
}
