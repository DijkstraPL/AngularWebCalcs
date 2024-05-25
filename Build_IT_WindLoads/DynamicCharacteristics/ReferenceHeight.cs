using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Fig.6.1]</remarks>
    public class ReferenceHeight : IFactor
    {
        #region Fields

        private readonly IStructure _building;
        private readonly ITerrain _terrain;

        #endregion // Fields

        #region Constructors
        
        public ReferenceHeight(IStructure building, ITerrain terrain)
        {
            _building = building;
            _terrain = terrain;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
       => Math.Max(_building.GetReferenceHeight(),
           _terrain.MinimumHeight);

        #endregion // Public_Methods
    }
}
