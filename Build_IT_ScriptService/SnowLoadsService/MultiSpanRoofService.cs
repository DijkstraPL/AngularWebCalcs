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
    [Export("SnowLoad-MultiSpanRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class MultiSpanRoofService : SnowLoadBaseService
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

        public MultiSpanRoofService()
        {
            Result = new Result(new Dictionary<string, string> {
                { "C_e_",          null },
                { "s_k_",          null },
                { "V",             null },
                { "C_esl_",        null },
                { "s_n_",          null },
                { "s_Ad_",         null },
                { "t_i_",          null },
                { "∆_t_",          null },
                { "U",             null },
                { "C_t_",          null },
                { "μ_1_(α_1_)",    null },
                { "μ_1_(α_2_)",    null },
                { "μ_2_(α)",       null },
                { "s(α_1_)",       null },
                { "s(α_2_)",       null },
                { "s_middle_",     null }
            });
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            MultiSpanRoof multiSpanRoof = GetMultiSpanRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            multiSpanRoof.CalculateSnowLoad();

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
            Result["μ_1_(α_1_)"] = multiSpanRoof.LeftRoof.ShapeCoefficient;
            Result["μ_1_(α_2_)"] = multiSpanRoof.RightRoof.ShapeCoefficient;
            Result["μ_2_(α)"] = multiSpanRoof.ShapeCoefficient;
            Result["s(α_1_)"] = multiSpanRoof.LeftRoof.SnowLoadOnRoofValue;
            Result["s(α_2_)"] = multiSpanRoof.RightRoof.SnowLoadOnRoofValue;
            Result["s_middle_"] = multiSpanRoof.SnowLoadOnMiddleRoof;

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private MultiSpanRoof GetMultiSpanRoof(Building building)
        {
            return new MultiSpanRoof(building, LeftSlope.Value, RightSlope.Value,
                LeftSnowFences.HasValue ? LeftSnowFences.Value : MultiSpanRoof.DefaultSnowFences,
                RightSnowFences.HasValue ? RightSnowFences.Value : MultiSpanRoof.DefaultSnowFences);
        }

        #endregion // Private_Methods
    }
}
