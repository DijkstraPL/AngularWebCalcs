using Build_IT_FrameStatica.Results.Interfaces;

namespace Build_IT_FrameStatica.Nodes.Interfaces
{
    public interface IShearForceProvider
    {
        #region Properties

        IValue VerticalForce { get; }

        #endregion // Properties
    }
}
