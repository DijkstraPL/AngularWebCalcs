using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_SnowLoads;
using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptService.SnowLoadsService
{
    [Export("SnowLoad-CylindricalRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class CylindricalRoofService : SnowLoadBaseService
    {
        #region Properties
        
        public Property<double> Width { get; } =
            new Property<double>("Width", 
                v => Convert.ToDouble(v));
        public Property<double> Height { get; } =
            new Property<double>("Height",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public CylindricalRoofService()
        {
            Result = new Result(new Dictionary<string, string> {
                { "C_e_",     null },
                { "s_k_",     null },
                { "V",        null },
                { "C_esl_",   null },
                { "s_n_",     null },
                { "s_Ad_",    null },
                { "t_i_",     null },
                { "∆_t_",     null },
                { "U",        null },
                { "C_t_",     null },
                { "l_s_",     null },
                { "μ_3_",     null },
                { "s_I_",     null },
                { "s_l,II_",  null },
                { "s_r,II_",  null }
            });
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override IResult Calculate()
        {
            IBuildingSite buildingSite = GetBuildingSite();
            ISnowLoad snowLoad = GetSnowLoad(buildingSite);
            IBuilding building = GetBuilding(snowLoad);
            CylindricalRoof cylindricalRoof = GetCylindricalRoof(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            cylindricalRoof.CalculateSnowLoad();
            cylindricalRoof.CalculateDriftLength();

            Result["C_e_"]= buildingSite.ExposureCoefficient;
            Result["s_k_"] = snowLoad.DefaultCharacteristicSnowLoad;
            Result["V"] = snowLoad.VariationCoefficient;
            Result["C_esl_"] = snowLoad.ExceptionalSnowLoadCoefficient;
            Result["s_n_"] = snowLoad.SnowLoadForSpecificReturnPeriod;
            Result["s_Ad_"] = snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod;
            Result["t_i_"] = building.InternalTemperature;
            Result["∆_t_"] = building.TempreatureDifference;
            Result["U"] = building.OverallHeatTransferCoefficient;
            Result["C_t_"] = building.ThermalCoefficient;
            Result["l_s_"] = cylindricalRoof.DriftLength;
            Result["μ_3_"] = cylindricalRoof.ShapeCoefficient;
            Result["s_I_"] = cylindricalRoof.RoofCasesSnowLoad[1];
            Result["s_l,II_"] = cylindricalRoof.RoofCasesSnowLoad[2];
            Result["s_r,II_"] = cylindricalRoof.RoofCasesSnowLoad[3];

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods
           
        private CylindricalRoof GetCylindricalRoof(IBuilding building)
        {
            return new CylindricalRoof(building, Width.Value, Height.Value);
        }

        #endregion // Private_Methods
    }
}
