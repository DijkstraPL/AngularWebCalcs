using Build_IT_CommonTools.Maths;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.WindLoadsCases.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_WindLoads.WindLoadsCases
{
    public abstract class WindLoadCase : IWindLoadCase
    {
        #region Fields

        protected readonly IStructure _building;
        protected readonly IWindLoadData _windLoadData;

        #endregion // Fields

        #region Constructors
        
        public WindLoadCase(IStructure building, IWindLoadData windLoadData)
        {
            _building = building;
            _windLoadData = windLoadData;
        }

        #endregion // Constructors

        #region Public_Methods

        public abstract IEnumerable<IDictionary<Field, double>> CalculatePressureCoefficients();
        public abstract IDictionary<Field, double> GetExternalPressureCoefficientsMax();
        public abstract IDictionary<Field, double> GetExternalPressureCoefficientsMin();
        public IDictionary<Field, double> GetExternalWindPressureMaxAt(double height) 
            => GetExternalWindPressures(height, GetExternalPressureCoefficientsMax());

        public IDictionary<Field, double> GetExternalWindPressureMinAt(double height)
            => GetExternalWindPressures(height, GetExternalPressureCoefficientsMin());

        #endregion // Public_Methods

        #region Protected_Methods

        protected double GetExternalCoefficientBetween(
            double externalPressureCoefficientOneMeter,
            double externalPressureCoefficientTenMeters,
            double area)
            => externalPressureCoefficientOneMeter -
                (externalPressureCoefficientOneMeter - externalPressureCoefficientTenMeters)
                * Math.Log10(area);
        
        protected IDictionary<Field, double> GetExternalPressureCoefficients(
            IDictionary<Field, double> externalPressureCoefficientsTenSquareMeters, 
            IDictionary<Field, double> externalPressureCoefficientsOneSquareMeter)
        {
            var externalPressureCoefficients = new Dictionary<Field, double>();
            foreach (var area in _building.Areas)
            {
                if (!externalPressureCoefficientsTenSquareMeters.ContainsKey(area.Key)
                    && !externalPressureCoefficientsOneSquareMeter.ContainsKey(area.Key))
                    continue;

                if (area.Value <= 1)
                    externalPressureCoefficients.Add(area.Key, externalPressureCoefficientsOneSquareMeter[area.Key]);
                else if (area.Value < 10)
                    externalPressureCoefficients.Add(area.Key,
                   GetExternalCoefficientBetween(
                       externalPressureCoefficientsOneSquareMeter[area.Key],
                       externalPressureCoefficientsTenSquareMeters[area.Key],
                       area.Value));
                else if (area.Value >= 10)
                    externalPressureCoefficients.Add(area.Key, externalPressureCoefficientsTenSquareMeters[area.Key]);
            }
            return externalPressureCoefficients;
        }

        protected IDictionary<Field, double> InterpolateBetweenFor(
            (double position, IDictionary<Field, double> values) endPosition,
            (double position, IDictionary<Field, double> values) startPosition,
            double ratio)
        {
            var externalPresssureCoefficients = new Dictionary<Field, double>();
            foreach (var keyValuePair in startPosition.values)
            {
                externalPresssureCoefficients.Add(
                    keyValuePair.Key,
                    Interpolation.InterpolateLinearBetween(
                        start: (startPosition.position, keyValuePair.Value),
                        end: (endPosition.position, endPosition.values[keyValuePair.Key]),
                        at: ratio)
                    );
            }
            return externalPresssureCoefficients;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private Dictionary<Field, double> GetExternalWindPressures(double height, IDictionary<Field, double> externalPressureCoefficients)
        {
            var externalWindPressure = new Dictionary<Field, double>();
            foreach (var externalPressureCoefficient in externalPressureCoefficients)
            {
                externalWindPressure.Add(externalPressureCoefficient.Key,
                    _windLoadData.GetPeakVelocityPressureAt(height) 
                    * externalPressureCoefficient.Value);
            }

            return externalWindPressure;
        }

        #endregion // Private_Methods
    }
}
