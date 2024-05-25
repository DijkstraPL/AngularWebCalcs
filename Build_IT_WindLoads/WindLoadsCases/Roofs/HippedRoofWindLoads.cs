using System;
using System.Collections.Generic;
using System.Text;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.BuildingData.Roofs;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public class HippedRoofWindLoads : WindLoadCase
    {
        #region Fields

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle5Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0 },
                {Field.G, 0 },
                {Field.H, 0 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.6 },
                {Field.L, -1.2 },
                {Field.M, -0.6 },
                {Field.N, -0.4 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle5Min =
            new Dictionary<Field, double>
            {
                {Field.F, -1.7 },
                {Field.G, -1.2 },
                {Field.H, -0.6},
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.6 },
                {Field.L, -1.2 },
                {Field.M, -0.6 },
                {Field.N, -0.4 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle5Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0 },
                {Field.G, 0 },
                {Field.H, 0 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.6 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.4 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle5Min =
            new Dictionary<Field, double>
            {
                {Field.F, -2.5 },
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.6 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.4 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle15Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.2 },
                {Field.G, 0.2 },
                {Field.H, 0.2 },
                {Field.I, -0.5 },
                {Field.J, -1 },
                {Field.K, -1.2 },
                {Field.L, -1.4 },
                {Field.M, -0.6 },
                {Field.N, -0.3 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle15Min =
            new Dictionary<Field, double>
            {
                {Field.F, -0.9 },
                {Field.G, -0.8 },
                {Field.H, -0.3 },
                {Field.I, -0.5 },
                {Field.J, -1 },
                {Field.K, -1.2 },
                {Field.L, -1.4 },
                {Field.M, -0.6 },
                {Field.N, -0.3 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle15Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.2 },
                {Field.G, 0.2 },
                {Field.H, 0.2 },
                {Field.I, -0.5 },
                {Field.J, -1.5 },
                {Field.K, -2 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.3 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle15Min =
            new Dictionary<Field, double>
            {
                {Field.F, -2 },
                {Field.G, -1.5 },
                {Field.H, -0.3 },
                {Field.I, -0.5 },
                {Field.J, -1.5 },
                {Field.K, -2 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.3 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle30Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.5 },
                {Field.G, 0.7 },
                {Field.H, 0.4 },
                {Field.I, -0.4 },
                {Field.J, -0.7 },
                {Field.K, -0.5 },
                {Field.L, -1.4 },
                {Field.M, -0.8 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle30Min =
            new Dictionary<Field, double>
            {
                {Field.F, -0.5 },
                {Field.G, -0.5 },
                {Field.H, -0.2 },
                {Field.I, -0.4 },
                {Field.J, -0.7 },
                {Field.K, -0.5 },
                {Field.L, -1.4 },
                {Field.M, -0.8 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle30Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.5 },
                {Field.G, 0.7 },
                {Field.H, 0.4 },
                {Field.I, -0.4 },
                {Field.J, -1.2 },
                {Field.K, -0.5 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle30Min =
            new Dictionary<Field, double>
            {
                {Field.F, -1.5 },
                {Field.G, -1.5 },
                {Field.H, -0.2 },
                {Field.I, -0.4 },
                {Field.J, -1.2 },
                {Field.K, -0.5 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle45Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.7 },
                {Field.G, 0.7 },
                {Field.H, 0.6 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -1.3 },
                {Field.M, -0.8 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle45Min =
            new Dictionary<Field, double>
            {
                {Field.F, 0 },
                {Field.G, 0 },
                {Field.H, 0 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -1.3 },
                {Field.M, -0.8 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle45Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.7 },
                {Field.G, 0.7 },
                {Field.H, 0.6 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.2 },
            };


        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle45Min =
            new Dictionary<Field, double>
            {
                {Field.F, 0 },
                {Field.G, 0 },
                {Field.H, 0 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -2 },
                {Field.M, -1.2 },
                {Field.N, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle60Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.7 },
                {Field.G, 0.7 },
                {Field.H, 0.7 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -1.2 },
                {Field.M, -0.4 },
                {Field.N, -0.2 },
            };

        protected IDictionary<Field, double> _ratioFor10SquareMetersAngle60Min
            => _ratioFor10SquareMetersAngle60Max;

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle60Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.7 },
                {Field.G, 0.7 },
                {Field.H, 0.7 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -2 },
                {Field.M, -0.4 },
                {Field.N, -0.2 },
            };

        protected IDictionary<Field, double> _ratioFor1SquareMeterAngle60Min
            => _ratioFor1SquareMeterAngle60Max;

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersAngle75Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.8 },
                {Field.G, 0.8 },
                {Field.H, 0.8 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -1.2 },
                {Field.M, -0.4 },
                {Field.N, -0.2 },
            };

        protected IDictionary<Field, double> _ratioFor10SquareMetersAngle75Min
            => _ratioFor10SquareMetersAngle75Max;

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterAngle75Max =
            new Dictionary<Field, double>
            {
                {Field.F, 0.8 },
                {Field.G, 0.8 },
                {Field.H, 0.8 },
                {Field.I, -0.3 },
                {Field.J, -0.6 },
                {Field.K, -0.3 },
                {Field.L, -2 },
                {Field.M, -0.4 },
                {Field.N, -0.2 },
            };

        protected IDictionary<Field, double> _ratioFor1SquareMeterAngle75Min
            => _ratioFor1SquareMeterAngle75Max;

        private IHippedRoof _hippedRoof => (IHippedRoof)_building;

        #endregion // Fields

        #region Constructors

        public HippedRoofWindLoads(IHippedRoof building, IWindLoadData windLoadData)
            : base(building, windLoadData)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override IEnumerable<IDictionary<Field, double>> CalculatePressureCoefficients()
        {
            throw new NotImplementedException();
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
            return GetExternalPressureCoefficient(
                           ratio5: _ratioFor10SquareMetersAngle5Max,
                           ratio15: _ratioFor10SquareMetersAngle15Max,
                           ratio30: _ratioFor10SquareMetersAngle30Max,
                           ratio45: _ratioFor10SquareMetersAngle45Max,
                           ratio60: _ratioFor10SquareMetersAngle60Max,
                           ratio75: _ratioFor10SquareMetersAngle75Max);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMax()
        {
            return GetExternalPressureCoefficient(
                           ratio5: _ratioFor1SquareMeterAngle5Max,
                           ratio15: _ratioFor1SquareMeterAngle15Max,
                           ratio30: _ratioFor1SquareMeterAngle30Max,
                           ratio45: _ratioFor1SquareMeterAngle45Max,
                           ratio60: _ratioFor1SquareMeterAngle60Max,
                           ratio75: _ratioFor1SquareMeterAngle75Max);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMin()
        {
            return GetExternalPressureCoefficient(
                           ratio5: _ratioFor10SquareMetersAngle5Min,
                           ratio15: _ratioFor10SquareMetersAngle15Min,
                           ratio30: _ratioFor10SquareMetersAngle30Min,
                           ratio45: _ratioFor10SquareMetersAngle45Min,
                           ratio60: _ratioFor10SquareMetersAngle60Min,
                           ratio75: _ratioFor10SquareMetersAngle75Min);
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMin()
        {
            return GetExternalPressureCoefficient(
                           ratio5: _ratioFor1SquareMeterAngle5Min,
                           ratio15: _ratioFor1SquareMeterAngle15Min,
                           ratio30: _ratioFor1SquareMeterAngle30Min,
                           ratio45: _ratioFor1SquareMeterAngle45Min,
                           ratio60: _ratioFor1SquareMeterAngle60Min,
                           ratio75: _ratioFor1SquareMeterAngle75Min);
        }

        private double GetAngle() 
            => _hippedRoof.CurrentRotation == HippedRoof.Rotation.Degrees_0 ?
                _hippedRoof.Angle0 : _hippedRoof.Angle90;

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

            double angle = GetAngle();
            if (ratios.ContainsKey(Math.Abs(angle)))
                return ratios[Math.Abs(angle)];

            return GetIntermediaryRatio(ratios);
        }
        
        private IDictionary<Field, double> GetIntermediaryRatio(
            Dictionary<double, IDictionary<Field, double>> ratios)
        {
            double angle = GetAngle();
            double previousAngleValue = 0;
            foreach (var ratio in ratios)
            {
                if (previousAngleValue == 0)
                {
                    previousAngleValue = ratio.Key;
                    continue;
                }

                if (Math.Abs(angle) < ratio.Key)
                    return InterpolateBetweenFor(
                        (ratio.Key, ratio.Value),
                        (previousAngleValue, ratios[previousAngleValue]),
                        Math.Abs(angle));

                previousAngleValue = ratio.Key;
            }
            throw new ArgumentOutOfRangeException();
        }
        #endregion // Private_Methods
    }
}
