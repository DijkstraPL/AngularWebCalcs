using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.WindLoadsCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.WindLoadsCases
{
    public class ExternalPressureWindForce
    {
        #region Fields

        private readonly IWindLoadData _windLoadData;
        private readonly IWindLoadCase _windLoadCase;
        private readonly IStructuralFactorCalculator _structuralFactorCalculator;

        #endregion // Fields

        #region Constructors

        public ExternalPressureWindForce(
            IWindLoadData windLoadData,
            IWindLoadCase windLoadCase,
            IStructuralFactorCalculator structuralFactorCalculator = null)
        {
            _windLoadData = windLoadData;
            _windLoadCase = windLoadCase;
            _structuralFactorCalculator = structuralFactorCalculator ?? new NoStructuralFactorCalculator();
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<IDictionary<Field, double>> CalculateExternalPressureWindForceAt(
             double height, bool calculateStructuralFactor)
        {
            foreach (var pressureCoefficients in _windLoadCase.CalculatePressureCoefficients())
                yield return GetExternalPressureWindForceFor(
                 pressureCoefficients,
                 calculateStructuralFactor);
        }

        public IDictionary<Field, double> GetExternalPressureWindForceMaxAt(
            double height, bool calculateStructuralFactor)
             => GetExternalPressureWindForceFor(
                 _windLoadCase.GetExternalWindPressureMaxAt(height),
                 calculateStructuralFactor);

        public IDictionary<Field, double> GetExternalPressureWindForceMinAt(
            double height, bool calculateStructuralFactor)
            => GetExternalPressureWindForceFor(
                _windLoadCase.GetExternalWindPressureMinAt(height),
                calculateStructuralFactor);

        #endregion // Public_Methods

        #region Private_Methods

        private IDictionary<Field, double> GetExternalPressureWindForceFor(
            IDictionary<Field, double> externalPressures, 
            bool calculateStructuralFactor)
        {
            var windLoadCases = new Dictionary<Field, double>();
            foreach (var externalPressure in externalPressures)
                windLoadCases.Add(externalPressure.Key,
                    _structuralFactorCalculator.GetStructuralFactor(calculateStructuralFactor) * 
                    externalPressure.Value);
            return windLoadCases;
        }

        #endregion // Private_Methods
    }
}
