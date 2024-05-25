using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// R_b
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.B.8]</remarks>
    public class AerodynamicAdmittanceWidth : IFactor
    {
        #region Fields

        private readonly IStructure _building;
        private readonly IFactor _referenceHeight;
        private readonly IFactorAt _turbulentLengthScale;
        private readonly IFactorAt _nondimensionalFrequency;

        #endregion // Fields

        #region Constructors

        public AerodynamicAdmittanceWidth(
            IStructure building,
            IFactor referenceHeight,
            IFactorAt turbulentLengthScale,
            IFactorAt nondimensionalFrequency)
        {
            _building = building;
            _referenceHeight = referenceHeight;
            _turbulentLengthScale = turbulentLengthScale;
            _nondimensionalFrequency = nondimensionalFrequency;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
        {
            double coefficient = GetCoefficient();
            if (coefficient == 0)
                return 1;
            return 1 / coefficient - 
                1 / (2 * Math.Pow(coefficient,2)) *
                (1 - Math.Pow(Math.E, -2 * coefficient));
        }

        #endregion // Public_Methods

        #region Private_Methods

        private double GetCoefficient()
        {
            double referenceHeight = _referenceHeight.GetFactor();
            return 4.6 * _building.Width /
                _turbulentLengthScale.GetFactorAt(referenceHeight) *
                _nondimensionalFrequency.GetFactorAt(referenceHeight);
        }

        #endregion // Private_Methods
    }
}
