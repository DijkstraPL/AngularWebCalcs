namespace Build_IT_FrameStatica.Loads.Interfaces
{
    public interface ILoadWithPosition : ILoad
    {
        #region Properties

        double Position { get; }

        #endregion // Properties
    }
}
