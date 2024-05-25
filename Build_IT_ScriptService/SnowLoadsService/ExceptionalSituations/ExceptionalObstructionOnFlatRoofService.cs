using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptService.SnowLoadsService.ExceptionalSituations
{
    [Export("SnowLoad-ExceptionalObstructionOnFlatRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    class ExceptionalObstructionOnFlatRoofService : SnowLoadBaseService
    {
        #region Properties
        
        public Property<double> LeftWidth { get; } =
            new Property<double>("LeftWidth",
                v => Convert.ToDouble(v));
        public Property<double> RightWidth { get; } =
            new Property<double>("RightWidth",
                v => Convert.ToDouble(v));
        public Property<double> LeftHeightDifference { get; } =
            new Property<double>("LeftHeightDifference",
                v => Convert.ToDouble(v));
        public Property<double> RightHeightDifference { get; } =
            new Property<double>("RightHeightDifference",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public ExceptionalObstructionOnFlatRoofService()
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
                { "l_s1_",   null },
                { "l_s2_",   null },
                { "μ_1_",    null },
                { "μ_2_",    null },
                { "s_1_",    null },
                { "s_2_",    null }
            });
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            ExceptionalObstructionOnFlatRoof exceptionalObstructionOnFlatRoof = GetExceptionalObstructionOnFlatRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            exceptionalObstructionOnFlatRoof.CalculateDriftLength();
            exceptionalObstructionOnFlatRoof.CalculateSnowLoad();

            Result["C_e_"] = buildingSite.ExposureCoefficient;
            Result["s_k_"] = snowLoad.DefaultCharacteristicSnowLoad;
            Result["V"] = snowLoad.VariationCoefficient;
            Result["C_esl_"] = snowLoad.ExceptionalSnowLoadCoefficient;
            Result["s_n_"] = snowLoad.SnowLoadForSpecificReturnPeriod;
            Result["s_Ad_"] = snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod;
            Result["t_i_"] = building.InternalTemperature;
            Result["∆_t_"] = building.TempreatureDifference;
            Result["U"] = building.OverallHeatTransferCoefficient;
            Result["C_t_"] = building.ThermalCoefficient;
            Result["l_s1_"] = exceptionalObstructionOnFlatRoof.LeftDriftLength;
            Result["l_s2_"] = exceptionalObstructionOnFlatRoof.RightDriftLength;
            Result["μ_1_"] = exceptionalObstructionOnFlatRoof.LeftShapeCoefficient;
            Result["μ_2_"] = exceptionalObstructionOnFlatRoof.RightShapeCoefficient;
            Result["s_1_"] = exceptionalObstructionOnFlatRoof.LeftSnowLoad;
            Result["s_2_"] = exceptionalObstructionOnFlatRoof.RightSnowLoad;

            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private ExceptionalObstructionOnFlatRoof GetExceptionalObstructionOnFlatRoof(Building building)
        {
            return new ExceptionalObstructionOnFlatRoof(building, LeftWidth.Value,
                RightWidth.Value, LeftHeightDifference.Value, RightHeightDifference.Value);
        }

        #endregion // Private_Methods
    }
}
