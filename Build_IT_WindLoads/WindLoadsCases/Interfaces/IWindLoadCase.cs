using Build_IT_WindLoads.BuildingData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.WindLoadsCases.Interfaces
{
    public interface IWindLoadCase
    {
        #region Public_Methods

        IEnumerable<IDictionary<Field, double>> CalculatePressureCoefficients();
        IDictionary<Field, double> GetExternalPressureCoefficientsMax();
        IDictionary<Field, double> GetExternalPressureCoefficientsMin();
        IDictionary<Field, double> GetExternalWindPressureMaxAt(double height);
        IDictionary<Field, double> GetExternalWindPressureMinAt(double height);

        #endregion // Public_Methods
    }
}
