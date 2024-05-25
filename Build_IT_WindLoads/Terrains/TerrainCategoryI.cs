using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using System;

namespace Build_IT_WindLoads.Terrains
{
    public class TerrainCategoryI : Terrain
    {
        #region Constructors

        public TerrainCategoryI(TerrainOrography terrainOrography = null)
            : base(roughnessLength: 0.01, minimumHeight: 1,
                  maximumHeight: 200, terrainOrography: terrainOrography)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double GetRoughnessFactorAt(double height)
        {
            height = CheckMinimumHeight(height);
            return 1.2 * Math.Pow(height / 10, 0.13);
        }

        #endregion // Public_Methods
    }
}
