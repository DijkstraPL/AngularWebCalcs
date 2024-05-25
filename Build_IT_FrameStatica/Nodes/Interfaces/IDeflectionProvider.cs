using Build_IT_FrameStatica.Results.Interfaces;

namespace Build_IT_FrameStatica.Nodes.Interfaces
{
    public interface IDeflectionProvider
    {
        #region Properties

        IValue HorizontalDeflection { get; }
        IValue VerticalDeflection { get; }

        #endregion // Properties
    }
}
