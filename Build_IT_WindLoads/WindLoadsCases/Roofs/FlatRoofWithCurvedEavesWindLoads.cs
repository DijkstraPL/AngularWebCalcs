using Build_IT_CommonTools;
using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public class FlatRoofWithCurvedEavesWindLoads : FlatRoofWindLoads
    {
        #region Fields

        private readonly IDictionary<Field, double> _ratio0_05For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1 },
                {Field.G, -1.2 },
                {Field.H, -0.4 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1 },
                {Field.G, -1.2 },
                {Field.H, -0.4 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.5 },
                {Field.G, -1.8 },
                {Field.H, -0.4 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.5 },
                {Field.G, -1.8 },
                {Field.H, -0.4 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -0.7 },
                {Field.G, -0.8 },
                {Field.H, -0.3 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -0.7 },
                {Field.G, -0.8 },
                {Field.H, -0.3 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.2 },
                {Field.G, -1.4 },
                {Field.H, -0.3 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.2 },
                {Field.G, -1.4 },
                {Field.H, -0.3 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_20For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -0.5 },
                {Field.G, -0.5 },
                {Field.H, -0.3 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_20For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -0.5 },
                {Field.G, -0.5 },
                {Field.H, -0.3 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_20For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -0.8 },
                {Field.G, -0.5 },
                {Field.H, -0.3 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_20For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -0.8 },
                {Field.G, -0.5 },
                {Field.H, -0.3 },
                {Field.I, -0.2 },
            };

        [Abbreviation("r")]
        [Unit("m")]
        public double Curvature { get; }

        #endregion // Fields

        #region Constructors

        public FlatRoofWithCurvedEavesWindLoads(IFlatRoofBuilding building,
            IWindLoadData windLoadData, double curvature)
            : base(building, windLoadData)
        {
            Curvature = curvature;
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
                ratio0_05: _ratio0_05For10SquareMetersMax,
                ratio0_10: _ratio0_10For10SquareMetersMax,
                ratio0_20: _ratio0_20For10SquareMetersMax);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMax()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor1SquareMeterMax,
                ratio0_05: _ratio0_05For1SquareMeterMax,
                ratio0_10: _ratio0_10For1SquareMeterMax,
                ratio0_20: _ratio0_20For1SquareMeterMax);

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMin()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor10SquareMetersMin,
                ratio0_05: _ratio0_05For10SquareMetersMin,
                ratio0_10: _ratio0_10For10SquareMetersMin,
                ratio0_20: _ratio0_20For10SquareMetersMin);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMin()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor1SquareMeterMin,
                ratio0_05: _ratio0_05For1SquareMeterMin,
                ratio0_10: _ratio0_10For1SquareMeterMin,
                ratio0_20: _ratio0_20For1SquareMeterMin);

        private IDictionary<Field, double> GetExternalPressureCoefficient(
            IDictionary<Field, double> ratio0,
            IDictionary<Field, double> ratio0_05,
            IDictionary<Field, double> ratio0_10,
            IDictionary<Field, double> ratio0_20)
        {
            double curvatureToBuildingHeightRatio =
               Curvature / _building.Height;

            if (curvatureToBuildingHeightRatio < 0)
                throw new ArgumentOutOfRangeException("Parapet height or building height is less than 0.");
            if (curvatureToBuildingHeightRatio == 0.05)
                return ratio0_05;
            else if (curvatureToBuildingHeightRatio == 0.10)
                return ratio0_10;
            else if (curvatureToBuildingHeightRatio == 0.20)
                return ratio0_20;
            else if (curvatureToBuildingHeightRatio < 0.05)
                return InterpolateBetweenFor(
                    (0.05, ratio0_05),
                    (0, ratio0),
                    curvatureToBuildingHeightRatio);
            else if (curvatureToBuildingHeightRatio < 0.10)
                return InterpolateBetweenFor(
                    (0.10, ratio0_10),
                    (0.05, ratio0_05),
                    curvatureToBuildingHeightRatio);
            else if (curvatureToBuildingHeightRatio < 0.20)
                return InterpolateBetweenFor(
                    (0.20, ratio0_20),
                    (0.10, ratio0_10),
                    curvatureToBuildingHeightRatio);

            throw new ArgumentOutOfRangeException();
        }

        #endregion // Private_Methods
    }
}
