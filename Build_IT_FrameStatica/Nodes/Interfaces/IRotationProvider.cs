using Build_IT_FrameStatica.Results.Interfaces;

namespace Build_IT_FrameStatica.Nodes.Interfaces
{
    public interface IRotationProvider
    {
        #region Properties

        IValue LeftRotation { get; }
        IValue RightRotation { get; }

        #endregion // Properties
    }
}
