using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public class FlatRoofWithParapetesWindLoads : FlatRoofWindLoads
    {
        #region Fields

        private readonly IDictionary<Field, double> _ratio0_025For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.6 },
                {Field.G, -1.1 },
                {Field.H, -0.7 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_025For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.6 },
                {Field.G, -1.1 },
                {Field.H, -0.7 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_025For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -2.2 },
                {Field.G, -1.8 },
                {Field.H, -1.2 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_025For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -2.2 },
                {Field.G, -1.8 },
                {Field.H, -1.2 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.4 },
                {Field.G, -0.9 },
                {Field.H, -0.7 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.4 },
                {Field.G, -0.9 },
                {Field.H, -0.7 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -2 },
                {Field.G, -1.6 },
                {Field.H, -1.2 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_05For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -2 },
                {Field.G, -1.6 },
                {Field.H, -1.2 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.2 },
                {Field.G, -0.8 },
                {Field.H, -0.7 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.2 },
                {Field.G, -0.8 },
                {Field.H, -0.7 },
                {Field.I, -0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.4 },
                {Field.H, -1.2 },
                {Field.I, 0.2 },
            };

        private readonly IDictionary<Field, double> _ratio0_10For1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.4 },
                {Field.H, -1.2 },
                {Field.I, -0.2 },
            };

        [Abbreviation("h_p")]
        [Unit("m")]
        public double ParapetHeight { get; }

        #endregion // Fields

        #region Constructors

        public FlatRoofWithParapetesWindLoads(IFlatRoofBuilding building, 
            IWindLoadData windLoadData, double parapetHeight)
            : base(building, windLoadData)
        {
            ParapetHeight = parapetHeight;
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
                ratio0_025: _ratio0_025For10SquareMetersMax,
                ratio0_05: _ratio0_05For10SquareMetersMax,
                ratio0_10: _ratio0_10For10SquareMetersMax);

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMax()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor1SquareMeterMax,
                ratio0_025: _ratio0_025For1SquareMeterMax,
                ratio0_05: _ratio0_05For1SquareMeterMax,
                ratio0_10: _ratio0_10For1SquareMeterMax);
       
        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMetersMin()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor10SquareMetersMin,
                ratio0_025: _ratio0_025For10SquareMetersMin,
                ratio0_05: _ratio0_05For10SquareMetersMin,
                ratio0_10: _ratio0_10For10SquareMetersMin);
        
        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeterMin()
            => GetExternalPressureCoefficient(
                ratio0: _ratioFor1SquareMeterMin,
                ratio0_025: _ratio0_025For1SquareMeterMin,
                ratio0_05: _ratio0_05For1SquareMeterMin,
                ratio0_10: _ratio0_10For1SquareMeterMin);
      
        private IDictionary<Field, double> GetExternalPressureCoefficient(
            IDictionary<Field, double> ratio0,
            IDictionary<Field, double> ratio0_025,
            IDictionary<Field, double> ratio0_05,
            IDictionary<Field, double> ratio0_10)
        {
            double parapetHeightToBuildingHeightRatio =
                ParapetHeight / _building.Height;

            if (parapetHeightToBuildingHeightRatio < 0)
                throw new ArgumentOutOfRangeException("Parapet height or building height is less than 0.");
            if (parapetHeightToBuildingHeightRatio == 0.025)
                return ratio0_025;
            else if (parapetHeightToBuildingHeightRatio == 0.05)
                return ratio0_05;
            else if (parapetHeightToBuildingHeightRatio == 0.10)
                return ratio0_10;
            else if (parapetHeightToBuildingHeightRatio < 0.025)
                return InterpolateBetweenFor(
                    (0.025, ratio0_025),
                    (0, ratio0),
                    parapetHeightToBuildingHeightRatio);
            else if (parapetHeightToBuildingHeightRatio < 0.05)
                return InterpolateBetweenFor(
                    (0.05, ratio0_05),
                    (0.025, ratio0_025),
                    parapetHeightToBuildingHeightRatio);
            else if (parapetHeightToBuildingHeightRatio < 0.10)
                return InterpolateBetweenFor(
                    (0.1, ratio0_10),
                    (0.05, ratio0_05),
                    parapetHeightToBuildingHeightRatio);

            throw new ArgumentOutOfRangeException();
        }

        #endregion // Private_Methods
    }
}
