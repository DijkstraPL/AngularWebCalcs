using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_SnowLoads;
using Build_IT_SnowLoads.BuildingTypes;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptService.SnowLoadsService
{
    [Export("SnowLoad-PitchedRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class PitchedRoofService : SnowLoadBaseService
    {
        #region Properties

        public Property<double> LeftSlope { get; } =
            new Property<double>("LeftSlope", 
                v => Convert.ToDouble(v));
        public Property<bool> LeftSnowFences { get; } =
            new Property<bool>("LeftSnowFences", 
                v => v.ToString() == "true");
        public Property<double> RightSlope { get; } =
            new Property<double>("RightSlope",
                v => Convert.ToDouble(v));
        public Property<bool> RightSnowFences { get; } =
            new Property<bool>("RightSnowFences",
                v => v.ToString() == "true");
        
        #endregion // Properties

        #region Constructors

        public PitchedRoofService()
        {
            Result = new Result(new Dictionary<string, string> {
                { "C_e_",        null },
                { "s_k_",        null },
                { "V",           null },
                { "C_esl_",      null },
                { "s_n_",        null },
                { "s_Ad_",       null },
                { "t_i_",        null },
                { "∆_t_",        null },
                { "U",           null },
                { "C_t_",        null },
                { "μ_l,1_",      null },
                { "μ_r,1_",      null },
                { "s_l,I_",      null },
                { "s_r,I_",      null },
                { "s_l,II_",     null },
                { "s_r,II_",     null },
                { "s_l,III_",    null },
                { "s_r,III_",    null }
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            PitchedRoof pitchedRoof = GetPitchedRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            pitchedRoof.CalculateSnowLoad();

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
            Result["μ_l,1_"]=pitchedRoof.LeftRoof.ShapeCoefficient;
            Result["μ_r,1_"]=pitchedRoof.RightRoof.ShapeCoefficient;
            Result["s_l,I_"]=pitchedRoof.LeftRoofCasesSnowLoad[1];
            Result["s_r,I_"]=pitchedRoof.RightRoofCasesSnowLoad[1];
            Result["s_l,II_"]=pitchedRoof.LeftRoofCasesSnowLoad[2];
            Result["s_r,II_"]=pitchedRoof.RightRoofCasesSnowLoad[2];
            Result["s_l,III_"]=pitchedRoof.LeftRoofCasesSnowLoad[3];
            Result["s_r,III_"]=pitchedRoof.RightRoofCasesSnowLoad[3];

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods
           
        private PitchedRoof GetPitchedRoof(Building building)
        {
            return new PitchedRoof(building, LeftSlope.Value, RightSlope.Value,
                LeftSnowFences.HasValue ? LeftSnowFences.Value : PitchedRoof.DefaultSnowFences,
                RightSnowFences.HasValue ? RightSnowFences.Value : PitchedRoof.DefaultSnowFences);
        }

        #endregion // Private_Methods
    }
}
