using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using System;

namespace Build_IT_WindLoads.Terrains
{
    public class TerrainCategory0 : Terrain
    {
        #region Constructors

        public TerrainCategory0(TerrainOrography terrainOrography = null) 
            : base(roughnessLength: 0.003, minimumHeight: 1,
                  maximumHeight: 200, terrainOrography: terrainOrography)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetRoughnessFactorAt(double height)
        {
            height = CheckMinimumHeight(height);
            return 1.3 * Math.Pow(height / 10, 0.11);
        }

        #endregion // Public_Methods
    }
}
