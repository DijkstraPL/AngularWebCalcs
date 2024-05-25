using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// ẟ
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.F.15]</remarks>
    public class LogarithmicDecrementOfDamping : IFactor
    {
        #region Fields

        private readonly IFactor _logarithmicDecrementOfStructuralDamping;
        private readonly IFactor _logarithmicDecrementOfAerodynamicDamping;
        private readonly IFactor _logarithmicDecrementOfDampingSpecialDevices;

        #endregion // Fields

        #region Constructors

        public LogarithmicDecrementOfDamping(
           IFactor logarithmicDecrementOfStructuralDamping,
           IFactor logarithmicDecrementOfAerodynamicDamping,
           IFactor logarithmicDecrementOfDampingSpecialDevices)
        {
            _logarithmicDecrementOfStructuralDamping = logarithmicDecrementOfStructuralDamping;
            _logarithmicDecrementOfAerodynamicDamping = logarithmicDecrementOfAerodynamicDamping;
            _logarithmicDecrementOfDampingSpecialDevices = logarithmicDecrementOfDampingSpecialDevices;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
         => (_logarithmicDecrementOfStructuralDamping?.GetFactor() ?? 0) +
             (_logarithmicDecrementOfAerodynamicDamping?.GetFactor() ?? 0) +
             (_logarithmicDecrementOfDampingSpecialDevices?.GetFactor() ?? 0);

        #endregion // Public_Methods
    }
}
