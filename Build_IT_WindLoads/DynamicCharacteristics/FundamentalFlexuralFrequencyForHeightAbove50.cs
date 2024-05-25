using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// n_1,x
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.F.2]</remarks>
    public class FundamentalFlexuralFrequencyForHeightAbove50 : IFactor
    {
        #region Fields

        private readonly IStructure _building;

        #endregion // Fields

        #region Constructors
        
        public FundamentalFlexuralFrequencyForHeightAbove50(IStructure building)
        {
            _building = building;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public double GetFactor()
        {
            // HACK: should be included
            //if (_building.Height >=  50) 
                return 46 / _building.Height;
            throw new ArgumentOutOfRangeException();
        }

        #endregion // Public_Methods
    }
}
