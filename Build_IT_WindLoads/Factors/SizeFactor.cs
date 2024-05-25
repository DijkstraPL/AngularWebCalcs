using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.Factors
{
    /// <summary>
    /// c_s
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.6.2]</remarks>
    public class SizeFactor : IFactor
    {
        #region Fields

        private readonly IFactor _referenceHeight;
        private readonly IWindLoadData _windLoadData;
        private readonly IFactor _backgroundFactor;

        #endregion // Fields

        #region Constructors
        
        public SizeFactor(IFactor referenceHeight,
        IWindLoadData windLoadData,
        IFactor backgroundFactor)
        {
            _referenceHeight = referenceHeight;
            _windLoadData = windLoadData;
            _backgroundFactor = backgroundFactor;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public double GetFactor()
        {
            var referenceHeight = _referenceHeight.GetFactor();
            var turbulenceIntensity = 
                _windLoadData.GetTurbulenceIntensityAt(
                    referenceHeight, adjustHeight: false);
            var backgroundFactor = _backgroundFactor.GetFactor();

            return (1 + 7 * turbulenceIntensity * Math.Sqrt(backgroundFactor)) /
                (1 + 7 * turbulenceIntensity);
        }

        #endregion // Public_Methods
    }
}
