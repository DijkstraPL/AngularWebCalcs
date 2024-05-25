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
    [Export("SnowLoad-MonopitchRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class MonopitchRoofService : SnowLoadBaseService
    {
        #region Properties

        public Property<double> Slope { get; } =
            new Property<double>("Slope",
                v => Convert.ToDouble(v));
        public Property<bool> SnowFences { get; } =
            new Property<bool>("SnowFences",
                v => v.ToString() == "true");

        #endregion // Properties

        #region Constructors

        public MonopitchRoofService()
        {
            Result = new Result(new Dictionary<string, string> {
                { "C_e_",      null },
                { "s_k_",      null },
                { "V",         null },
                { "C_esl_",    null },
                { "s_n_",      null },
                { "s_Ad_",     null },
                { "t_i_",      null },
                { "∆_t_",      null },
                { "U",         null },
                { "C_t_",      null },
                { "μ_1_",      null },
                { "s",         null }
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            MonopitchRoof monopitchRoof = GetMonopitchRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            monopitchRoof.CalculateSnowLoad();

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
            Result["μ_1_"] = monopitchRoof.ShapeCoefficient;
            Result["s"] = monopitchRoof.SnowLoadOnRoofValue;

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private MonopitchRoof GetMonopitchRoof(Building building)
        {
            if (SnowFences.HasValue)
                return new MonopitchRoof(building, Slope.Value, SnowFences.Value);
            return new MonopitchRoof(building, Slope.Value);
        }

        #endregion // Private_Methods
    }
}
