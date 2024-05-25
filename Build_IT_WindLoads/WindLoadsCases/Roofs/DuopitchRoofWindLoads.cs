using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.WindLoadsCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public abstract class DuopitchRoofWindLoads : WindLoadCase
    {
        #region Properties
        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus45Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus45Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus45Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus45Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus30Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus30Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus30Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus30Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus15Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus15Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus15Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus15Min { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus5Max { get; }

        protected abstract IDictionary<Field, double> RatioFor10SquareMetersAngleMinus5Min { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus5Max { get; }

        protected abstract IDictionary<Field, double> RatioFor1SquareMeterAngleMinus5Min { get; }


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

        public static DuopitchRoofWindLoads Create(
            IDuopitchRoof building, IWindLoadData windLoadData)
        {
            if (building.CurrentRotation == DuopitchRoof.Rotation.Degrees_0)
                return new DuopitchRoofWindLoadsRotation0(building, windLoadData);
            if (building.CurrentRotation == DuopitchRoof.Rotation.Degrees_90)
                return new DuopitchRoofWindLoadsRotation90(building, windLoadData);
            throw new ArgumentException(nameof(building.CurrentRotation));
        }

        public DuopitchRoofWindLoads(
            IDuopitchRoof building, IWindLoadData windLoadData)
            : base(building, windLoadData)
        {
            Angle = building.Angle;
            if (Angle < 5 && Angle > -5 || Angle > 75 || Angle < -45)
                throw new ArgumentOutOfRangeException(nameof(Angle));
        }

        #endregion // Constructors

        #region Public_Methods

        //TODO: Check that
        public override IEnumerable<IDictionary<Field, double>> CalculatePressureCoefficients()
        {
            var maxCoefficients = GetExternalPressureCoefficientsMax();
            var minCoefficients = GetExternalPressureCoefficientsMin();

            yield return maxCoefficients
                .ToDictionary(c => c.Key, c => c.Value >= 0 ? c.Value : 0);
            yield return minCoefficients
                .ToDictionary(c => c.Key, c => c.Value <= 0 ? c.Value : 0);

            var maxCoefficientsDict1 = maxCoefficients.Where(c =>
             c.Key == Field.F || c.Key == Field.G || c.Key == Field.H)
             .ToDictionary(c => c.Key, c => c.Value >= 0 ? c.Value : 0);
            var minCoefficientsDict1 = minCoefficients.Where(c =>
            c.Key == Field.I || c.Key == Field.J)
            .ToDictionary(c => c.Key, c => c.Value <= 0 ? c.Value : 0);

            yield return maxCoefficientsDict1.Concat(minCoefficientsDict1)
                .ToDictionary(c => c.Key, c => c.Value);

            var maxCoefficientsDict2 = maxCoefficients.Where(c =>
             c.Key == Field.F || c.Key == Field.G || c.Key == Field.H)
             .ToDictionary(c => c.Key, c => c.Value <= 0 ? c.Value : 0);
            var minCoefficientsDict2 = minCoefficients.Where(c =>
            c.Key == Field.I || c.Key == Field.J)
            .ToDictionary(c => c.Key, c => c.Value >= 0 ? c.Value : 0);

            yield return maxCoefficientsDict2.Concat(minCoefficientsDict2)
                .ToDictionary(c => c.Key, c => c.Value);
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
        {
            if (Angle > 5)
                return GetExternalPressureCoefficient(
                               ratio5: RatioFor10SquareMetersAngle5Max,
                               ratio15: RatioFor10SquareMetersAngle15Max,
                               ratio30: RatioFor10SquareMetersAngle30Max,
                               ratio45: RatioFor10SquareMetersAngle45Max,
                               ratio60: RatioFor10SquareMetersAngle60Max,
                               ratio75: RatioFor10SquareMetersAngle75Max);
            return GetExternalPressureCoefficient(
                           ratio5: RatioFor10SquareMetersAngleMinus5Max,
                           ratio15: RatioFor10SquareMetersAngleMinus15Max,
                           ratio30: RatioFor10SquareMetersAngleMinus30Max,
                           ratio45: RatioFor10SquareMetersAngleMinus45Max);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMax()
        {
            if (Angle > 5)
                return GetExternalPressureCoefficient(
                           ratio5: RatioFor1SquareMeterAngle5Max,
                           ratio15: RatioFor1SquareMeterAngle15Max,
                           ratio30: RatioFor1SquareMeterAngle30Max,
                           ratio45: RatioFor1SquareMeterAngle45Max,
                           ratio60: RatioFor1SquareMeterAngle60Max,
                           ratio75: RatioFor1SquareMeterAngle75Max);
            return GetExternalPressureCoefficient(
                           ratio5: RatioFor1SquareMeterAngleMinus5Max,
                           ratio15: RatioFor1SquareMeterAngleMinus15Max,
                           ratio30: RatioFor1SquareMeterAngleMinus30Max,
                           ratio45: RatioFor1SquareMeterAngleMinus45Max);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMin()
        {
            if (Angle > 5)
                return GetExternalPressureCoefficient(
                           ratio5: RatioFor10SquareMetersAngle5Min,
                           ratio15: RatioFor10SquareMetersAngle15Min,
                           ratio30: RatioFor10SquareMetersAngle30Min,
                           ratio45: RatioFor10SquareMetersAngle45Min,
                           ratio60: RatioFor10SquareMetersAngle60Min,
                           ratio75: RatioFor10SquareMetersAngle75Min);
            return GetExternalPressureCoefficient(
                       ratio5: RatioFor10SquareMetersAngleMinus5Min,
                       ratio15: RatioFor10SquareMetersAngleMinus15Min,
                       ratio30: RatioFor10SquareMetersAngleMinus30Min,
                       ratio45: RatioFor10SquareMetersAngleMinus45Min);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMin()
        {
            if (Angle > 5)
                return GetExternalPressureCoefficient(
                           ratio5: RatioFor1SquareMeterAngle5Min,
                           ratio15: RatioFor1SquareMeterAngle15Min,
                           ratio30: RatioFor1SquareMeterAngle30Min,
                           ratio45: RatioFor1SquareMeterAngle45Min,
                           ratio60: RatioFor1SquareMeterAngle60Min,
                           ratio75: RatioFor1SquareMeterAngle75Min);
            return GetExternalPressureCoefficient(
                       ratio5: RatioFor1SquareMeterAngleMinus5Min,
                       ratio15: RatioFor1SquareMeterAngleMinus15Min,
                       ratio30: RatioFor1SquareMeterAngleMinus30Min,
                       ratio45: RatioFor1SquareMeterAngleMinus45Min);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficient(
            IDictionary<Field, double> ratio5,
            IDictionary<Field, double> ratio15,
            IDictionary<Field, double> ratio30,
            IDictionary<Field, double> ratio45,
            IDictionary<Field, double> ratio60 = null,
            IDictionary<Field, double> ratio75 = null)
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

            if (ratios.ContainsKey(Math.Abs(Angle)))
                return ratios[Math.Abs(Angle)];

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

                if (Math.Abs(Angle) < ratio.Key)
                    return InterpolateBetweenFor(
                        (ratio.Key, ratio.Value),
                        (previousAngleValue, ratios[previousAngleValue]),
                        Math.Abs(Angle));

                previousAngleValue = ratio.Key;
            }
            throw new ArgumentOutOfRangeException();
        }

        #endregion // Private_Methods
    }
}
