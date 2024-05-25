using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.Factors
{
    /// <summary>
    /// c_d
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.6.3]</remarks>
    public class DynamicFactor : IFactor
    {
        #region Fields

        private readonly IFactor _referenceHeight;
        private readonly IWindLoadData _windLoadData;
        private readonly IFactor _peakFactor;
        private readonly IFactor _backgroundFactor;
        private readonly IFactor _resonanceResponse;

        #endregion // Fields

        #region Constructors

        public DynamicFactor(IFactor referenceHeight,
            IWindLoadData windLoadData,
            IFactor peakFactor,
            IFactor backgroundFactor,
            IFactor resonanceResponse)
        {
            _referenceHeight = referenceHeight;
            _windLoadData = windLoadData;
            _peakFactor = peakFactor;
            _backgroundFactor = backgroundFactor;
            _resonanceResponse = resonanceResponse;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public double GetFactor()
        {
            var referenceHeight = _referenceHeight.GetFactor();
            var turbulenceIntensity = 
                _windLoadData.GetTurbulenceIntensityAt(
                    referenceHeight, adjustHeight:false);
            var peakFactor = _peakFactor.GetFactor();
            var backgroundFactor = _backgroundFactor.GetFactor();
            var resonanceResponse = _resonanceResponse.GetFactor();

            return (1 + 2 * peakFactor * turbulenceIntensity *
                Math.Sqrt(backgroundFactor + resonanceResponse)) /
                (1 + 7 * turbulenceIntensity * Math.Sqrt(backgroundFactor));
        }

        #endregion // Public_Methods
    }
}
