using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using System;

namespace Build_IT_WindLoads.Terrains
{
    public class TerrainCategoryIII : Terrain
    {
        #region Constructors

        public TerrainCategoryIII(TerrainOrography terrainOrography = null)
            : base(roughnessLength: 0.3, minimumHeight: 5,
                  maximumHeight: 400, terrainOrography: terrainOrography)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetRoughnessFactorAt(double height)
        {
            height = CheckMinimumHeight(height);
            return 0.8 * Math.Pow(height / 10, 0.19);
        }

        #endregion // Public_Methods
    }
}
