using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.Factors
{
    public class StructuralFactor : IFactor
    {
        #region Fields

        private readonly IFactor _sizeFactor;
        private readonly IFactor _dynamicFactor;

        #endregion // Fields

        #region Constructors

        public StructuralFactor(
            IFactor sizeFactor,
            IFactor dynamicFactor)
        {
            _sizeFactor = sizeFactor;
            _dynamicFactor = dynamicFactor;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor() 
            => _sizeFactor.GetFactor() * _dynamicFactor.GetFactor();

        #endregion // Public_Methods
    }
}
