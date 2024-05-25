using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Displacements
{
    internal class Rotation : Displacement
    {
        #region Constructors

        public Rotation() : base()
        {
        }

        public Rotation(ISpan span, double position) : base(span, position)
        {
        }

        #endregion // Constructors
    }
}
