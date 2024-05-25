using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptService.SnowLoadsService.ExceptionalSituations
{
    [Export("SnowLoad-ExceptionalMultiSpanRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class ExceptionalMultiSpanRoofService : SnowLoadBaseService
    {
        #region Properties
        
        public Property<double> LeftDriftLength { get; } =
            new Property<double>("LeftDriftLength",
                v => Convert.ToDouble(v));
        public Property<double> RightDriftLength { get; } =
            new Property<double>("RightDriftLength",
                v => Convert.ToDouble(v));
        public Property<double> HeightInTheLowestPart { get; } =
            new Property<double>("HeightInTheLowestPart",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public ExceptionalMultiSpanRoofService()
        {
            Result = new Result(new Dictionary<string, string> {
                { "C_e_",    null },
                { "s_k_",    null },
                { "V",       null },
                { "C_esl_",  null },
                { "s_n_",    null },
                { "s_Ad_",   null },
                { "t_i_",    null },
                { "∆_t_",    null },
                { "U",       null },
                { "C_t_",    null },
                { "b_3_",    null },
                { "μ_1_",    null },
                { "s",       null }
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            ExceptionalMultiSpanRoof exceptionalMultiSpanRoof = GetExceptionalMultiSpanRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            exceptionalMultiSpanRoof.CalculateSnowLoad();

            Result["C_e_"]=buildingSite.ExposureCoefficient;
            Result["s_k_"]=snowLoad.DefaultCharacteristicSnowLoad;
            Result["V"]=snowLoad.VariationCoefficient;
            Result["C_esl_"]=snowLoad.ExceptionalSnowLoadCoefficient;
            Result["s_n_"]=snowLoad.SnowLoadForSpecificReturnPeriod;
            Result["s_Ad_"]=snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod;
            Result["t_i_"]=building.InternalTemperature;
            Result["∆_t_"]=building.TempreatureDifference;
            Result["U"]=building.OverallHeatTransferCoefficient;
            Result["C_t_"]=building.ThermalCoefficient;
            Result["b_3_"]=exceptionalMultiSpanRoof.HorizontalDimensionOfThreeSlopes;
            Result["μ_1_"]=exceptionalMultiSpanRoof.ShapeCoefficient;
            Result["s"]=exceptionalMultiSpanRoof.SnowLoad;

            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private ExceptionalMultiSpanRoof GetExceptionalMultiSpanRoof(Building building)
        {
            return new ExceptionalMultiSpanRoof(building, LeftDriftLength.Value,
                RightDriftLength.Value, HeightInTheLowestPart.Value);
        }

        #endregion // Private_Methods
    }
}
