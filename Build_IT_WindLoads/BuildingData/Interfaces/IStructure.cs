using Build_IT_CommonTools.Attributes;
using System.Collections.Generic;

namespace Build_IT_WindLoads.BuildingData.Interfaces
{
    public interface IStructure
    {
        #region Properties

        [Abbreviation("d")]
        [Unit("m")]
        double Length { get; }
        [Abbreviation("b")]
        [Unit("m")]
        double Width { get; }
        [Abbreviation("h")]
        [Unit("m")]
        double Height { get; }
        
        IDictionary<Field, double> Areas { get; }

        #endregion // Properties

        #region Public_Method

        double GetReferenceHeight();

        #endregion // Public_Method
    }
}