using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// R^2
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.B.6]</remarks>
    public class ResonanceResponse : IFactor
    {
        #region Fields

        private readonly IFactor _referenceHeight;
        private readonly IFactor _logarithmicDecrementOfDamping;
        private readonly IFactorAt _nonDimensionalPowerSpectralDensity;
        private readonly IFactor _aerodynamicAdmittanceWidth;
        private readonly IFactor _aerodynamicAdmittanceHeight;

        #endregion // Fields

        #region Constructors

        public ResonanceResponse(IFactor referenceHeight,
            IFactor logarithmicDecrementOfDamping,
            IFactorAt nonDimensionalPowerSpectralDensity,
            IFactor aerodynamicAdmittanceWidth,
            IFactor aerodynamicAdmittanceHeight)
        {
            _referenceHeight = referenceHeight;
            _logarithmicDecrementOfDamping = logarithmicDecrementOfDamping;
            _nonDimensionalPowerSpectralDensity = nonDimensionalPowerSpectralDensity;
            _aerodynamicAdmittanceWidth = aerodynamicAdmittanceWidth;
            _aerodynamicAdmittanceHeight = aerodynamicAdmittanceHeight;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
        {
            var referenceHeight = _referenceHeight.GetFactor();
            double logarithmicDecrementOfDamping = _logarithmicDecrementOfDamping.GetFactor();
            double nonDimensionalPowerSpectralDensity =
                _nonDimensionalPowerSpectralDensity.GetFactorAt(referenceHeight);
            double aerodynamicAdmittanceWidth = _aerodynamicAdmittanceWidth.GetFactor();
            double aerodynamicAdmittanceHeight = _aerodynamicAdmittanceHeight.GetFactor();

            return Math.Pow(Math.PI, 2) /
                (2 * logarithmicDecrementOfDamping) *
                nonDimensionalPowerSpectralDensity *
                aerodynamicAdmittanceHeight *
                aerodynamicAdmittanceWidth;
        }

        #endregion // Public_Methods
    }
}
