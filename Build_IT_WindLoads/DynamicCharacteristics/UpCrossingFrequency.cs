using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// v
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.B.5]</remarks>
    public class UpCrossingFrequency : IFactor
    {
        #region Fields

        private readonly IFactor _fundamentalFlexuralFrequency;
        private readonly IFactor _backgroundFactor;
        private readonly IFactor _resonanceResponse;

        private readonly double _minimalUpCrossingFrequency = 0.08;

        #endregion // Fields

        #region Constructors
        
        public UpCrossingFrequency(IFactor fundamentalFlexuralFrequency,
            IFactor backgroundFactor, 
            IFactor resonanceResponse)
        {
            _fundamentalFlexuralFrequency = fundamentalFlexuralFrequency;
            _backgroundFactor = backgroundFactor;
            _resonanceResponse = resonanceResponse;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
        {
            var fundamentalFlexuralFrequency = _fundamentalFlexuralFrequency.GetFactor();
            var backgroundFactor = _backgroundFactor.GetFactor();
            var resonanceResponse = _resonanceResponse.GetFactor();

            var upCrossingFrequency = fundamentalFlexuralFrequency * 
                Math.Sqrt(resonanceResponse / 
                (backgroundFactor + resonanceResponse));

            return Math.Max(upCrossingFrequency, _minimalUpCrossingFrequency);
        }

        #endregion // Public_Methods
    }
}
