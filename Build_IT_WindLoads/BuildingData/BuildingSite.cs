using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains.Interfaces;
using System;

namespace Build_IT_WindLoads.BuildingData
{
    public class BuildingSite : IBuildingSite
    {
        #region Properties

        [Abbreviation("a")]
        [Unit("m")]
        public double HeightAboveSeaLevel { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Fig.NB.1]</remarks>
        public WindZone WindZone { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Tab.4.1]</remarks>
        public ITerrain Terrain { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Tab.NB.1]</remarks>
        [Abbreviation("v_b,0")]
        [Unit("m/s")]
        public double FundamentalValueBasicWindVelocity { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 NB.1.5]</remarks>
        [Abbreviation("c_season")]
        [Unit("-")]
        public double SeasonalFactor => _seasonalFactor?.GetFactor() ?? 1;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 NB.1.5]</remarks>
        [Abbreviation("c_dir")]
        [Unit("-")]
        public double DirectionalFactor => _directionalFactor?.GetFactor() ?? 1;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Eq.4.1]</remarks>
        public double BasicWindVelocity => SeasonalFactor * DirectionalFactor * FundamentalValueBasicWindVelocity;

        #endregion // Properties

        #region Fields

        private readonly IFactor _seasonalFactor;
        private readonly IFactor _directionalFactor;

        #endregion // Fields

        #region Constructors

        public BuildingSite(double heightAboveSeaLevel, WindZone windZone, ITerrain terrain,
            IFactor seasonalFactor = null, IFactor directionalFactor = null)
        {
            HeightAboveSeaLevel = heightAboveSeaLevel;
            WindZone = windZone;
            Terrain = terrain;
            _seasonalFactor = seasonalFactor;
            _directionalFactor = directionalFactor;

            FundamentalValueBasicWindVelocity = GetFundamentalValueBasicWindVelocity();
        }

        #endregion // Constructors

        #region Private_Methods

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Tab.NB.1]</remarks>
        private double GetFundamentalValueBasicWindVelocity()
        {
            switch (WindZone)
            {
                case WindZone.I:
                case WindZone.III:
                case WindZone.I_III:
                    return GetFundamentalValueBasicWindVelocityForZoneIAndIII();
                case WindZone.II:
                    return 26;
                case WindZone.I_II:
                    return (26 + GetFundamentalValueBasicWindVelocityForZoneIAndIII()) / 2;
                default:
                    throw new ArgumentException("Wrong wind zone");
            }
        }

        private double GetFundamentalValueBasicWindVelocityForZoneIAndIII()
        {
            if (HeightAboveSeaLevel <= 300)
                return 22;
            else
                return 22 * (1 + 0.0006 * (HeightAboveSeaLevel - 300));
        }

        #endregion // Private_Methods
    }
}
