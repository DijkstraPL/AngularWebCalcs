namespace Build_IT_FrameStatica.Loads.ContinuousLoads.Interfaces
{
    public interface IForceResult
    {
        #region Public_Methods

        double GetValue(double distanceFromLoadStartPosition);

        #endregion // Public_Methods
    }
}
