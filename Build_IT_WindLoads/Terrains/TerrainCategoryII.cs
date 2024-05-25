using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using System;

namespace Build_IT_WindLoads.Terrains
{
    public class TerrainCategoryII : Terrain
    {
        #region Constructors

        public TerrainCategoryII(TerrainOrography terrainOrography = null)
            : base(roughnessLength: 0.05, minimumHeight: 2,
                  maximumHeight: 300, terrainOrography: terrainOrography)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double GetRoughnessFactorAt(double height)
        {
            height = CheckMinimumHeight(height);
            return 1.0 * Math.Pow(height / 10, 0.17);
        }

        #endregion // Public_Methods
    }
}
