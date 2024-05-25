using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// B^2
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.B.3]</remarks>
    public class BackgroundFactor : IFactor
    {
        #region Fields

        private readonly IStructure _building;
        private readonly IFactor _referenceHeight;
        private readonly IFactorAt _turbulentLengthScale;

        #endregion // Fields

        #region Constructors
        
        public BackgroundFactor(IStructure building, 
            IFactor referenceHeight,
            IFactorAt turbulentLengthScale)
        {
            _building = building;
            _referenceHeight = referenceHeight;
            _turbulentLengthScale = turbulentLengthScale;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
        {
            var referenceHeight = _referenceHeight.GetFactor();
            var turbulentLengthScale = _turbulentLengthScale.GetFactorAt(referenceHeight);
            return 1 / (1 + 0.9 *
                Math.Pow((_building.Width + _building.Height)
                    / turbulentLengthScale, 0.63));
        }

        #endregion // Public_Methods
    }
}
