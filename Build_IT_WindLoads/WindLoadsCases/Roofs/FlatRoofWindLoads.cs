using System;
using System.Collections.Generic;
using System.Text;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public class FlatRoofWindLoads : WindLoadCase
    {
        #region Fields
        
        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersMax =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.2 },
                {Field.H, -0.7 },
                {Field.I, 0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor10SquareMetersMin =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.2 },
                {Field.H, -0.7 },
                {Field.I, -0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterMax =
            new Dictionary<Field, double>
            {
                {Field.F, -2.5 },
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, 0.2 },
            };

        protected readonly IDictionary<Field, double> _ratioFor1SquareMeterMin =
            new Dictionary<Field, double>
            {
                {Field.F, -2.5 },
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, -0.2 },
            };

        #endregion // Fields

        #region Constructors

        public FlatRoofWindLoads(IFlatRoofBuilding building, IWindLoadData windLoadData)
            : base(building, windLoadData)
        {
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
                _ratioFor10SquareMetersMax, _ratioFor1SquareMeterMax);

        public override IDictionary<Field, double> GetExternalPressureCoefficientsMin() 
            => GetExternalPressureCoefficients(
                _ratioFor10SquareMetersMin, _ratioFor1SquareMeterMin);

        #endregion // Public_Methods

        #region Private_Methods


        #endregion // Private_Methods
    }
}
