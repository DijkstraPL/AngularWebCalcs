using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Displacements
{
    internal class HorizontalDeflection : Displacement
    {
        #region Constructors

        public HorizontalDeflection() : base()
        {
        }

        public HorizontalDeflection(ISpan span, double position) : base(span, position)
        {
        }

        #endregion // Constructors
    }
}
