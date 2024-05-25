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
    [Export("SnowLoad-ExceptionalOverDoorOrLoadingBay", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    class ExceptionalOverDoorOrLoadingBayService : SnowLoadBaseService
    {
        #region Properties

        public Property<double> WidthAboveDoor { get; } =
            new Property<double>("WidthAboveDoor",
                v => Convert.ToDouble(v));
        public Property<double> HeightDifference { get; } =
            new Property<double>("HeightDifference",
                v => Convert.ToDouble(v));
        public Property<double> BuildingWidth { get; } =
            new Property<double>("BuildingWidth",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public ExceptionalOverDoorOrLoadingBayService()
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
                { "l_s_",    null },
                { "μ_1_",    null },
                { "s_1_",    null },
            });
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            ExceptionalOverDoorOrLoadingBay exceptionalOverDoorOrLoadingBay = GetExceptionalOverDoorOrLoadingBay(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            exceptionalOverDoorOrLoadingBay.CalculateDriftLength();
            exceptionalOverDoorOrLoadingBay.CalculateSnowLoad();

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
            Result["l_s_"] = exceptionalOverDoorOrLoadingBay.DriftLength;
            Result["μ_1_"] = exceptionalOverDoorOrLoadingBay.ShapeCoefficient;
            Result["s_1_"] = exceptionalOverDoorOrLoadingBay.SnowLoad;

            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private ExceptionalOverDoorOrLoadingBay GetExceptionalOverDoorOrLoadingBay(Building building)
        {
            return new ExceptionalOverDoorOrLoadingBay(building, WidthAboveDoor.Value,
                BuildingWidth.Value, HeightDifference.Value);
        }

        #endregion // Private_Methods
    }
}
