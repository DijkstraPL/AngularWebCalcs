using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Loads.ContinuousLoads.Interfaces
{
    public interface IDisplacementResult
    {
        #region Public_Methods

        double GetValue(ISpan span, double distanceFromLeftSide, double currentLength);

        #endregion // Public_Methods
    }
}
