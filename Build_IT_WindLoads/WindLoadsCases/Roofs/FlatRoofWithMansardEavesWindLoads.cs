using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public class FlatRoofWithMansardEavesWindLoads : FlatRoofWindLoads
    {
        #region Fields

        private readonly IDictionary<Field, double> _ratio30For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1 },
                {Field.G, -1 },
                {Field.H, -0.3 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio30For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1 },
                {Field.G, -1 },
                {Field.H, -0.3 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio30For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.5 },
                {Field.G, -1.5 },
                {Field.H, -0.3 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio30For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.5 },
                {Field.G, -1.5 },
                {Field.H, -0.3 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio45For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.2 },
                {Field.G, -1.3 },
                {Field.H, -0.4 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio45For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.2 },
                {Field.G, -1.3 },
                {Field.H, -0.4 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio45For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.9 },
                {Field.H, -0.4 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio45For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.9 },
                {Field.H, -0.4 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio60For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.3 },
                {Field.G, -1.3 },
                {Field.H, -0.5 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio60For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.3 },
                {Field.G, -1.3 },
                {Field.H, -0.5 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio60For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.9 },
                {Field.G, -1.9 },
                {Field.H, -0.5 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio60For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.9 },
                {Field.G, -1.9 },
                {Field.H, -0.5 },
                {Field.I, -0.2 },
            };

        [Abbreviation("α")]
        [Unit("°")]
        public double Angle { get; }

        #endregion // Fields

        #region Constructors

        public FlatRoofWithMansardEavesWindLoads(IFlatRoofBuilding building,
            IWindLoadData windLoadData, double angle)
            : base(building, windLoadData)
        {
            Angle = angle;
        }

        #endregion // Constructors

        #region Public_Methods

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
                ratio0: _ratioFor10SquareMetersMax,
                ratio30: _ratio30For10SquareMetersMax,
                ratio45: _ratio45For10SquareMetersMax,
                ratio60: _ratio60For10SquareMetersMax);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMax()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor1SquareMeterMax,
                ratio30: _ratio30For1SquareMeterMax,
                ratio45: _ratio45For1SquareMeterMax,
                ratio60: _ratio60For1SquareMeterMax);

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMin()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor10SquareMetersMin,
                ratio30: _ratio30For10SquareMetersMin,
                ratio45: _ratio45For10SquareMetersMin,
                ratio60: _ratio60For10SquareMetersMin);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMin()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor1SquareMeterMin,
                ratio30: _ratio30For1SquareMeterMin,
                ratio45: _ratio45For1SquareMeterMin,
                ratio60: _ratio60For1SquareMeterMin);

        private IDictionary<Field, double> GetExternalPressureCoefficient(
            IDictionary<Field, double> ratio0,
            IDictionary<Field, double> ratio30,
            IDictionary<Field, double> ratio45,
            IDictionary<Field, double> ratio60)
        {
            if (Angle < 30)
                throw new ArgumentOutOfRangeException(nameof(Angle));
            if (Angle == 30)
                return ratio30;
            else if (Angle == 45)
                return ratio45;
            else if (Angle == 60)
                return ratio60;
            else if (Angle < 45)
                return InterpolateBetweenFor(
                    (45, ratio45),
                    (30, ratio30),
                    Angle);
            else if (Angle < 60)
                return InterpolateBetweenFor(
                    (60, ratio60),
                    (45, ratio45),
                    Angle);
            else if (Angle > 60)
                return InterpolateBetweenFor(
                    (90, ratio0),
                    (60, ratio60),
                    Angle);

            throw new ArgumentOutOfRangeException();
        }

        #endregion // Private_Methods
    }
}
