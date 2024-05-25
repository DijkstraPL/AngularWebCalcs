using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// f_L(z,n)
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 B.1.(2)]</remarks>
    public class NonDimensionalFrequency : IFactorAt
    {
        #region Fields

        private readonly IWindLoadData _windLoadData;
        private readonly IFactor _fundamentalFlexuralFrequency;
        private readonly IFactorAt _turbulentLengthScale;

        #endregion // Fields

        #region Constructors

        public NonDimensionalFrequency(IWindLoadData windLoadData,
            IFactor fundamentalFlexuralFrequency,
            IFactorAt turbulentLengthScale)
        {
            _windLoadData = windLoadData;
            _fundamentalFlexuralFrequency = fundamentalFlexuralFrequency;
            _turbulentLengthScale = turbulentLengthScale;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactorAt(double height)
        {
            var fundamentalFlexuralFrequency  = _fundamentalFlexuralFrequency.GetFactor();
            var turbulentLengthScale = _turbulentLengthScale.GetFactorAt(height);
            var meanWindVelocity = _windLoadData.GetMeanWindVelocityAt(height, adjustHeight:false);
            return fundamentalFlexuralFrequency * turbulentLengthScale / meanWindVelocity;
        }

        #endregion // Public_Methods
    }
}
