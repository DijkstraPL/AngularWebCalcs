using Build_IT_FrameStatica.Results.Interfaces;

namespace Build_IT_FrameStatica.Nodes.Interfaces
{
    public interface IBendingMomentProvider
    {
        #region Properties

        IValue BendingMoment { get; }

        #endregion // Properties
    }
}
