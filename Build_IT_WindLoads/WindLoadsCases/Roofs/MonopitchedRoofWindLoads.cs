using System;
using System.Collections.Generic;
using System.Text;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.WindLoadsCases.Interfaces;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public abstract class MonopitchedRoofWindLoads : WindLoadCase
    {
        #region Properties

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle5Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle5Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle5Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle5Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle15Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle15Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle15Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle15Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle30Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle30Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle30Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle30Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle45Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle45Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle45Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle45Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle60Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle60Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle60Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle60Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle75Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngle75Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle75Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngle75Min { get; }

        public double Angle { get; }

        #endregion // Properties

        #region Constructors

        public static MonopitchedRoofWindLoads Create(
            IMonopitchRoof building, IWindLoadData windLoadData)
        {
            if (building.CurrentRotation == MonopitchRoof.Rotation.Degrees_0)
                return new MonopitchedRoofWindLoadsRotation0(building, windLoadData);
            if (building.CurrentRotation == MonopitchRoof.Rotation.Degrees_90)
                return new MonopitchedRoofWindLoadsRotation90(building, windLoadData);
            if (building.CurrentRotation == MonopitchRoof.Rotation.Degrees_180)
                return new MonopitchedRoofWindLoadsRotation180(building, windLoadData);
            throw new ArgumentException(nameof(building.CurrentRotation));
        }

        public MonopitchedRoofWindLoads(
            IMonopitchRoof building, IWindLoadData windLoadData)
            : base(building, windLoadData)
        {
            Angle = building.Angle;
        }

        #endregion // Constructors

        #region Public_Methods
        public override IEnumerable<IDictionary<Field, double>> CalculatePressureCoefficients()
        {
            yield return GetExternalPressureCoefficientsMax();
            yield return GetExternalPressureCoefficientsMin();
        }

        public override IDictionary<Field, double> GetExternalPressureCoefficientsMax()
            => GetExternalPressureCoefficients(
                GetExternalPressureCoefficientTenSquareMetersMax(),
                GetExternalPressureCoefficientOneSquareMeterMax());

        public override IDictionary<Field, double> GetExternalPressureCoefficientsMin()
            => GetExternalPressureCoefficients(
                GetExternalPressureCoefficientTenSquareMetersMin(),
                GetExternalPressureCoefficientOneSquareMeterMin());

        #endregion // Public_Methods

        #region Private_Methods

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMax()
            => GetExternalPressureCoefficient(
                ratio5: RatioFor10SquareMetersAngle5Max,
                ratio15: RatioFor10SquareMetersAngle15Max,
                ratio30: RatioFor10SquareMetersAngle30Max,
                ratio45: RatioFor10SquareMetersAngle45Max,
                ratio60: RatioFor10SquareMetersAngle60Max,
                ratio75: RatioFor10SquareMetersAngle75Max);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMax()
            => GetExternalPressureCoefficient(
                ratio5: RatioFor1SquareMeterAngle5Max,
                ratio15: RatioFor1SquareMeterAngle15Max,
                ratio30: RatioFor1SquareMeterAngle30Max,
                ratio45: RatioFor1SquareMeterAngle45Max,
                ratio60: RatioFor1SquareMeterAngle60Max,
                ratio75: RatioFor1SquareMeterAngle75Max);

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMin()
            => GetExternalPressureCoefficient(
                ratio5: RatioFor10SquareMetersAngle5Min,
                ratio15: RatioFor10SquareMetersAngle15Min,
                ratio30: RatioFor10SquareMetersAngle30Min,
                ratio45: RatioFor10SquareMetersAngle45Min,
                ratio60: RatioFor10SquareMetersAngle60Min,
                ratio75: RatioFor10SquareMetersAngle75Min);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMin()
            => GetExternalPressureCoefficient(
                ratio5: RatioFor1SquareMeterAngle5Min,
                ratio15: RatioFor1SquareMeterAngle15Min,
                ratio30: RatioFor1SquareMeterAngle30Min,
                ratio45: RatioFor1SquareMeterAngle45Min,
                ratio60: RatioFor1SquareMeterAngle60Min,
                ratio75: RatioFor1SquareMeterAngle75Min);

        private IDictionary<Field, double> GetExternalPressureCoefficient(
            IDictionary<Field, double> ratio5,
            IDictionary<Field, double> ratio15,
            IDictionary<Field, double> ratio30,
            IDictionary<Field, double> ratio45,
            IDictionary<Field, double> ratio60,
            IDictionary<Field, double> ratio75)
        {
            var ratios = new Dictionary<double, IDictionary<Field, double>>()
               {
                   {5, ratio5 },
                   {15, ratio15 },
                   {30, ratio30 },
                   {45, ratio45 },
                   {60, ratio60 },
                   {75, ratio75 }
               };

            if (Angle < 5)
                throw new ArgumentOutOfRangeException(nameof(Angle));
            if (ratios.ContainsKey(Angle))
                return ratios[Angle];

            return GetIntermediaryRatio(ratios);
        }

        private IDictionary<Field, double> GetIntermediaryRatio(Dictionary<double, IDictionary<Field, double>> ratios)
        {
            double previousAngleValue = 0;
            foreach (var ratio in ratios)
            {
                if (previousAngleValue == 0)
                {
                    previousAngleValue = ratio.Key;
                    continue;
                }

                if (Angle < ratio.Key)
                    return InterpolateBetweenFor(
                        (ratio.Key, ratio.Value),
                        (previousAngleValue, ratios[previousAngleValue]),
                        Angle);

                previousAngleValue = ratio.Key;
            }
            throw new ArgumentOutOfRangeException();
        }

        #endregion // Private_Methods
    }
}
