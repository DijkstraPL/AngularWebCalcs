﻿using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptService.SnowLoadsService.ExceptionalSituations
{
    [Export("SnowLoad-ExceptionalSnowBehindParapetAtEaves", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-3")]
    [ExportMetadata("Type", "SnowLoad")]
    public class ExceptionalSnowBehindParapetAtEavesService : SnowLoadBaseService
    {
        #region Properties

        public Property<double> RidgeDistance { get; } =
            new Property<double>("RidgeDistance",
                v => Convert.ToDouble(v));
        public Property<double> HeightDifference { get; } =
            new Property<double>("HeightDifference",
                v => Convert.ToDouble(v));
        public Property<double> BuildingWidth { get; } =
            new Property<double>("BuildingWidth",
                v => Convert.ToDouble(v));
        
        #endregion // Properties

        #region Constructors

        public ExceptionalSnowBehindParapetAtEavesService()
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
                { "s",       null },
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            BuildingSite buildingSite = GetBuildingSite();
            SnowLoad snowLoad = GetSnowLoad(buildingSite);
            Building building = GetBuilding(snowLoad);
            ExceptionalSnowBehindParapetAtEaves exceptionalSnowBehindParapetAtEaves
                = GetExceptionalSnowBehindParapetAtEaves(building);

            if (!ExposureCoefficient.HasValue)
                buildingSite.CalculateExposureCoefficient();
            snowLoad.CalculateSnowLoad();
            building.CalculateThermalCoefficient();
            exceptionalSnowBehindParapetAtEaves.CalculateDriftLength();
            exceptionalSnowBehindParapetAtEaves.CalculateSnowLoad();

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
            Result["l_s_"]=exceptionalSnowBehindParapetAtEaves.DriftLength;
            Result["μ_1_"]=exceptionalSnowBehindParapetAtEaves.ShapeCoefficient;
            Result["s"]=exceptionalSnowBehindParapetAtEaves.SnowLoad;

            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private ExceptionalSnowBehindParapetAtEaves GetExceptionalSnowBehindParapetAtEaves(Building building)
        {
            return new ExceptionalSnowBehindParapetAtEaves(building, RidgeDistance.Value,
                BuildingWidth.Value, HeightDifference.Value);
        }

        #endregion // Private_Methods
    }
}