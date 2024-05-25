using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// S_L(z,n)
    /// </summary>
    public class NonDimensionalPowerSpectralDensity : IFactorAt
    {
        #region Fields

        private readonly IFactorAt _nonDimensionalFrequency;

        #endregion // Fields

        #region Constructors
        
        public NonDimensionalPowerSpectralDensity(IFactorAt nonDimensionalFrequency)
        {
            _nonDimensionalFrequency = nonDimensionalFrequency;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactorAt(double height)
        {
            double nonDimensionalFrequency = _nonDimensionalFrequency.GetFactorAt(height);

            return (6.8 * nonDimensionalFrequency) /
                (Math.Pow(1 + 10.2 * nonDimensionalFrequency, 5.0 / 3));
        }

        #endregion // Public_Methods
    }
}
