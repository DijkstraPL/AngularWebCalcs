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
    [Export("SnowLoad-ExceptionalRoofAbuttingToTallerConstruction", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class ExceptionalRoofAbuttingToTallerConstructionService : SnowLoadBaseService
    {
        #region Properties

        public Property<double> HeightDifference { get; } =
            new Property<double>("HeightDifference",
                v => Convert.ToDouble(v));
        public Property<double> LowerBuildingWidth { get; } =
            new Property<double>("LowerBuildingWidth",
                v => Convert.ToDouble(v));
        public Property<double> UpperBuildingWidth { get; } =
            new Property<double>("UpperBuildingWidth",
                v => Convert.ToDouble(v));
        public Property<double> Angle { get; } =
            new Property<double>("Angle",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public ExceptionalRoofAbuttingToTallerConstructionService()
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
                { "μ_3_",    null },
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
            ExceptionalRoofAbuttingToTallerConstruction exceptionalRoofAbuttingToTallerConstruction 
                = GetExceptionalRoofAbuttingToTallerConstruction(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            exceptionalRoofAbuttingToTallerConstruction.CalculateDriftLength();
            exceptionalRoofAbuttingToTallerConstruction.CalculateSnowLoad();

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
            Result["l_s_"]=exceptionalRoofAbuttingToTallerConstruction.DriftLength;
            Result["μ_3_"]=exceptionalRoofAbuttingToTallerConstruction.ShapeCoefficient3;
            Result["μ_1_"]=exceptionalRoofAbuttingToTallerConstruction.ShapeCoefficient1;
            Result["μ_2_"]=exceptionalRoofAbuttingToTallerConstruction.ShapeCoefficient2;
            Result["s_1_"]=exceptionalRoofAbuttingToTallerConstruction.SnowLoadNearTheTop;
            Result["s_2_"]=exceptionalRoofAbuttingToTallerConstruction.SnowLoadNearTheEdge;

            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private ExceptionalRoofAbuttingToTallerConstruction GetExceptionalRoofAbuttingToTallerConstruction(Building building)
        {
            return new ExceptionalRoofAbuttingToTallerConstruction(building, UpperBuildingWidth.Value,
                LowerBuildingWidth.Value, HeightDifference.Value, Angle.Value);
        }

        #endregion // Private_Methods
    }
}
