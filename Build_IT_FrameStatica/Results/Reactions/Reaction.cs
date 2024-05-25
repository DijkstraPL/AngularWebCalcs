using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Results.Reactions
{
    internal abstract class Reaction : IResultValue
    {
        #region Properties

        public double Value { get; set; }
        public double Position { get; }
        public ISpan Span{ get; }

        #endregion // Properties

        #region Constructors

        protected Reaction()
        {
        }

        protected Reaction(ISpan span, double position)
        {
            Position = position;
            Span = span;
        }

        #endregion // Constructors

        #region Public_Methods

        public override string ToString() => Value.ToString();

        #endregion // Public_Methods
    }
}
