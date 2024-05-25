using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.Factors.Interfaces;

namespace Build_IT_WindLoads.Terrains.Interfaces
{
    public interface ITerrain
    {
        #region Properties

        IFactorAt TerrainOrography { get; }

        [Abbreviation("z_min")]
        [Unit("m")]
        double MinimumHeight { get; }

        [Abbreviation("z_0")]
        [Unit("m")]
        double RoughnessLength { get; }

        IFactor HeightDisplacement { get; }

        #endregion // Properties

        #region Public_Methods

        double GetRoughnessFactorAt(double height);

        #endregion // Public_Methods
    }
}