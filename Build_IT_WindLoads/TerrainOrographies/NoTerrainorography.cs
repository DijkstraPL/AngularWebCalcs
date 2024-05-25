using Build_IT_WindLoads.Factors.Interfaces;

namespace Build_IT_WindLoads.TerrainOrographies
{
    internal class NoTerrainOrography : IFactorAt
    {
        #region Public_Methods

        public double GetFactorAt(double verticalDistanceFromCrestTop)
            => 1;

        #endregion // Public_Methods
    }
}