using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using Build_IT_WindLoads.Terrains.Enums;
using Build_IT_WindLoads.Terrains.Interfaces;
using System;

namespace Build_IT_WindLoads.Terrains
{
    public abstract class Terrain : ITerrain
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 4.2.2.(1)]</remarks>
        [Abbreviation("z_0,II")]
        [Unit("m")]
        public const double RoughnessLengthInSecondTerrainType = 0.05;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Tab.4.1]</remarks>
        [Abbreviation("z_0")]
        [Unit("m")]
        public double RoughnessLength { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Tab.4.1]</remarks>
        [Abbreviation("z_min")]
        [Unit("m")]
        public double MinimumHeight { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 4.2.2.(1)]</remarks>
        [Abbreviation("z_max")]
        [Unit("m")]
        public double MaximumHeight { get; }

        public IFactorAt TerrainOrography { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Eq.4.5]</remarks>
        [Abbreviation("k_r")]
        [Unit("")]
        public double TerrainFactor 
            => 0.19 * Math.Pow(RoughnessLength / RoughnessLengthInSecondTerrainType, 0.07);

        public IFactor HeightDisplacement { get; protected set; }

        #endregion // Properties

        #region Factories

        public static Terrain Create(
            TerrainType terrainType,
            IFactor heightDisplacement = null,
            TerrainOrography terrainOrography = null)
        {
            switch (terrainType)
            {
                case TerrainType.Terrain_0:
                    return new TerrainCategory0(terrainOrography);
                case TerrainType.Terrain_I:
                    return new TerrainCategoryI(terrainOrography);
                case TerrainType.Terrain_II:
                    return new TerrainCategoryII(terrainOrography);
                case TerrainType.Terrain_III:
                    return new TerrainCategoryIII(terrainOrography);
                case TerrainType.Terrain_IV:
                    return new TerrainCategoryIV(heightDisplacement, terrainOrography);
                default:
                    throw new ArgumentException(nameof(terrainType));
            }
        }

        #endregion // Factories

        #region Constructors

        protected Terrain(
            double roughnessLength,
            double minimumHeight, 
            double maximumHeight, 
            IFactorAt terrainOrography)
        {
            TerrainOrography = terrainOrography ?? new NoTerrainOrography();
            RoughnessLength = roughnessLength;
            MinimumHeight = minimumHeight;
            MaximumHeight = maximumHeight;
        }

        #endregion // Constructors

        #region Public_Methods

        /// <summary>
        /// c_r(z)
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Eq.4.4]</remarks>
        public virtual double GetRoughnessFactorAt(double height)
        {
            height = CheckMinimumHeight(height);

            return TerrainFactor * Math.Log(height / RoughnessLength);
        }

        #endregion // Public_Methods

        #region Protected_Methods

        protected double CheckMinimumHeight(double height)
        {
            if (height > MaximumHeight)
                throw new ArgumentOutOfRangeException("Height is too large.");
            if (height < MinimumHeight)
                height = MinimumHeight;
            return height;
        }

        #endregion // Protected_Methods
    }
}
