using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using System;

namespace Build_IT_WindLoads.Terrains
{
    public class TerrainCategoryIV : Terrain
    {
        #region Constructors

        public TerrainCategoryIV(IFactor heightDisplacement,
            TerrainOrography terrainOrography = null)
            : base(roughnessLength: 1, minimumHeight: 10,
                  maximumHeight: 500, terrainOrography: terrainOrography)
        {
            HeightDisplacement = heightDisplacement ?? 
                throw new ArgumentNullException(nameof(heightDisplacement));
        }
        
        #endregion // Constructors

        #region Public_Methods

        public override double GetRoughnessFactorAt(double height)
        {
            height = CheckMinimumHeight(height);
            return 0.6 * Math.Pow(height / 10, 0.24);
        }

        #endregion // Public_Methods
    }
}
