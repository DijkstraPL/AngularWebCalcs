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
    [Export("SnowLoad-SnowOverhanging", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class SnowOverhangingService : SnowLoadBaseService
    {
        #region Properties
        
        public Property<double> SnowLayerDepth { get; } =
            new Property<double>("SnowLayerDepth", 
                v => Convert.ToDouble(v));
        public Property<double> SnowLoadOnRoofValue { get; } =
            new Property<double>("SnowLoadOnRoofValue",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public SnowOverhangingService()
        {
            Result = new Result(new Dictionary<string, string> {
                {"C_e_",   null },
                {"s_k_",   null },
                {"V",      null },
                {"C_esl_", null },
                {"s_n_",   null },
                {"s_Ad_",  null },
                {"t_i_",   null },
                {"∆_t_",   null },
                {"U",      null },
                {"C_t_",   null },
                {"k",      null },
                {"s_e_",   null }
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            SnowOverhanging snowOverhanging = GetSnowOverhanging(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            snowOverhanging.CalculateSnowLoad();

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
            Result["k"]=snowOverhanging.IrregularShapeCoefficient;
            Result["s_e_"]=snowOverhanging.SnowLoad;

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods
           
        private SnowOverhanging GetSnowOverhanging(Building building)
        {
            if (SnowLoadOnRoofValue.HasValue && !SnowLayerDepth.HasValue)
                return new SnowOverhanging(building, SnowLoadOnRoofValue.Value);
            else if(SnowLoadOnRoofValue.HasValue && SnowLayerDepth.HasValue)
                return new SnowOverhanging(building, SnowLayerDepth.Value, SnowLoadOnRoofValue.Value);

            throw new ArgumentException();
        }

        #endregion // Private_Methods
    }
}
