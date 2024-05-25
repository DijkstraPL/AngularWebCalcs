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
    [Export("SnowLoad-RoofAbuttingToTallerConstruction", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class RoofAbuttingToTallerConstructionService : SnowLoadBaseService
    {
        #region Properties
        
        public Property<double> UpperBuildingWidth { get; } =
            new Property<double>("UpperBuildingWidth", 
                v => Convert.ToDouble(v));
        public Property<double> LowerBuildingWidth { get; } =
            new Property<double>("LowerBuildingWidth",
                v => Convert.ToDouble(v));
        public Property<double> HeightDifference { get; } =
            new Property<double>("HeightDifference",
                v => Convert.ToDouble(v));
        public Property<double> HigherRoofSlope { get; } =
            new Property<double>("HigherRoofSlope",
                v => Convert.ToDouble(v));
        public Property<bool> HigherRoofSnowFences { get; } =
            new Property<bool>("HigherRoofSnowFences",
                v => v.ToString() == "true");
        
        #endregion // Properties

        #region Constructors

        public RoofAbuttingToTallerConstructionService()
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
                { "μ_s_",      null },
                { "μ_w_",      null },
                { "μ_2_",      null },
                { "μ_1_",      null },
                { "l_s_",      null },
                { "s_I_",      null },
                { "s_l,II_",   null },
                { "s_r,II_",   null }
            });
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            RoofAbuttingToTallerConstruction roofAbuttingToTallerConstruction = GetRoofAbuttingToTallerConstruction(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            roofAbuttingToTallerConstruction.CalculateSnowLoad();

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
            Result["μ_s_"]=roofAbuttingToTallerConstruction.ShapeCoefficientSlidingSnow;
            Result["μ_w_"]=roofAbuttingToTallerConstruction.ShapeCoefficientWind;
            Result["μ_2_"]=roofAbuttingToTallerConstruction.ShapeCoefficient;
            Result["μ_1_"]=roofAbuttingToTallerConstruction.ShapeCoefficientAtTheEnd;
            Result["l_s_"]=roofAbuttingToTallerConstruction.DriftLength;
            Result["s_I_"]=roofAbuttingToTallerConstruction.SnowLoadsNearTallerBuilding[1];
            Result["s_l,II_"]=roofAbuttingToTallerConstruction.SnowLoadsNearTallerBuilding[2];
            Result["s_r,II_"]=roofAbuttingToTallerConstruction.SnowLoadOnRoofValueAtTheEnd;

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods
           
        private RoofAbuttingToTallerConstruction GetRoofAbuttingToTallerConstruction(Building building)
        {
            return new RoofAbuttingToTallerConstruction(building, 
                UpperBuildingWidth.Value, LowerBuildingWidth.Value,
                HeightDifference.Value, HigherRoofSlope.Value, 
                HigherRoofSnowFences.HasValue ? HigherRoofSnowFences.Value : RoofAbuttingToTallerConstruction.DefaultSnowFences);
        }

        #endregion // Private_Methods
    }
}
