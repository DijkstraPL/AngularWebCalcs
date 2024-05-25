using System.Collections.Generic;

namespace Build_IT_WindLoads.BuildingData.Interfaces
{
    public interface IWall
    {
        #region Properties
        
        IDictionary<Field, double> Areas { get; }

        #endregion // Properties
    }
}