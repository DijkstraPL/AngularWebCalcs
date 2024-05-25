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
    [Export("SnowLoad-ExceptionalObstructionOnPitchedOrCurvedRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    class ExceptionalObstructionOnPitchedOrCurvedRoofService : SnowLoadBaseService
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

        public ExceptionalObstructionOnPitchedOrCurvedRoofService()
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
            ExceptionalObstructionOnPitchedOrCurvedRoof exceptionalObstructionOnPitchedOrCurvedRoof 
                = GetExceptionalObstructionOnPitchedOrCurvedRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            exceptionalObstructionOnPitchedOrCurvedRoof.CalculateDriftLength();
            exceptionalObstructionOnPitchedOrCurvedRoof.CalculateSnowLoad();

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
            Result["l_s1_"]=exceptionalObstructionOnPitchedOrCurvedRoof.LeftDriftLength;
            Result["l_s2_"]=exceptionalObstructionOnPitchedOrCurvedRoof.RightDriftLength;
            Result["μ_1_"]=exceptionalObstructionOnPitchedOrCurvedRoof.LeftShapeCoefficient;
            Result["μ_2_"]=exceptionalObstructionOnPitchedOrCurvedRoof.RightShapeCoefficient;
            Result["s_1_"]=exceptionalObstructionOnPitchedOrCurvedRoof.LeftSnowLoad;
            Result["s_2_"]=exceptionalObstructionOnPitchedOrCurvedRoof.RightSnowLoad;

            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private ExceptionalObstructionOnPitchedOrCurvedRoof GetExceptionalObstructionOnPitchedOrCurvedRoof(Building building)
        {
            return new ExceptionalObstructionOnPitchedOrCurvedRoof(building, LeftWidth.Value,
                RightWidth.Value, LeftHeightDifference.Value, RightHeightDifference.Value);
        }

        #endregion // Private_Methods
    }
}
