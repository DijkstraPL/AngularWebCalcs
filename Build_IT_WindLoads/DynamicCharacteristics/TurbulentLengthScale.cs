using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains.Interfaces;
using System;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// L(z)
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.B.1]</remarks>
    public class TurbulentLengthScale : IFactorAt
    {
        #region Fields

        private readonly ITerrain _terrain;
        [Abbreviation("z_t")]
        [Unit("m")]
        private readonly double _referenceHeight = 200;
        [Abbreviation("L_t")]
        [Unit("m")]
        private readonly double _referenceLengthScale = 300;

        #endregion // Fields

        #region Constructors
        
        public TurbulentLengthScale(ITerrain terrain)
        {
            _terrain = terrain;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public double GetFactorAt(double height)
        {
            if (height > 200)
                throw new ArgumentOutOfRangeException(nameof(height));
            if (height < _terrain.MinimumHeight)
                return GetFactorAt(_terrain.MinimumHeight);
            return _referenceLengthScale *
                Math.Pow((height / _referenceHeight),
                0.67 + 0.05 * Math.Log(_terrain.RoughnessLength));
        }

        #endregion // Public_Methods
    }
}
