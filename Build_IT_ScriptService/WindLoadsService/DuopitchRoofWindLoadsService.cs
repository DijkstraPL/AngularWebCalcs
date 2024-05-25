using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.TerrainOrographies;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.WindLoadsCases;
using Build_IT_WindLoads.WindLoadsCases.Roofs;
using System;
using System.Collections.Generic;
using System.Composition;

namespace Build_IT_ScriptService.WindLoadsService
{
    [Export("WindLoad-DuopitchRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-4")]
    [ExportMetadata("Type", "WindLoad")]
    public class DuopitchRoofWindLoadsService : WindLoadBaseService
    {
        #region Properties

        public Property<double> BuildingLength { get; } =
            new Property<double>("BuildingLength",
                v => Convert.ToDouble(v));
        public Property<double> BuildingWidth { get; } =
            new Property<double>("BuildingWidth",
                v => Convert.ToDouble(v));
        public Property<double> BuildingInnerHeight { get; } =
            new Property<double>("BuildingInnerHeight",
                v => Convert.ToDouble(v));
        public Property<double> BuildingOuterHeight { get; } =
            new Property<double>("BuildingOuterHeight",
                v => Convert.ToDouble(v));

        public Property<double> ReferenceHeight { get; } =
            new Property<double>("ReferenceHeight",
                v => Convert.ToDouble(v));

        public Property<DuopitchRoof.Rotation> BuildingOrientation { get; } =
            new Property<DuopitchRoof.Rotation>("BuildingOrientation",
                v => Enum.Parse<DuopitchRoof.Rotation>(v.ToString()), required: false);

        #endregion // Properties

        #region Constructors

        public DuopitchRoofWindLoadsService()
        {
            Result = new Result(new Dictionary<string, string>() {
                { "e", null },
                { "v_b,0_", null },
                { "c_dir_", null },
                { "v_b_", null },
                { "c_r_(z_e_)", null },
                { "c_0_(z_e_)", null },
                { "v_m_(z_e_)", null },
                { "I_v_(z_e_)", null },
                { "q_p_(z_e_)", null },
                { "c_sc,d_", null },
                { "w_e,F,max_", null },
                { "w_e,G,max_", null },
                { "w_e,H,max_", null },
                { "w_e,I,max_", null },
                { "w_e,J,max_", null },
                { "w_e,F,min_", null },
                { "w_e,G,min_", null },
                { "w_e,H,min_", null },
                { "w_e,I,min_", null },
                { "w_e,J,min_", null },
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            DuopitchRoof structureData = GetStructureData();
            HeightDisplacement heightDisplacement = GetHeightDisplacement(structureData);
            TerrainOrography terrainOrography = GetTerrainOrography();
            Terrain terrain = GetTerrainCategory(heightDisplacement, terrainOrography);
            DirectionalFactor directionalFactor = GetDirectionalFactor();
            BuildingSite buildingSite = GetBuildingSite(terrain, directionalFactor);
            ReferenceHeightDueToNeighbouringStructures referenceHeightDueToNeighbouringStructures
                = GetReferenceHeightDueToNeighbouringStructures();
            WindLoadData windLoadData = GetWindLoadData(buildingSite, structureData, referenceHeightDueToNeighbouringStructures);
            DuopitchRoofWindLoads wallsWindLoads = GetDuopitchRoofWindLoads(structureData, windLoadData);
            StructuralFactorCalculator structuralFactorCalculator =
                GetStructuralFactorCalculator(structureData, terrain, windLoadData);
            ExternalPressureWindForce externalPressureWindForce =
                GetExternalPressureWindForce(windLoadData, wallsWindLoads, structuralFactorCalculator);

            var referenceHeight = ReferenceHeight.HasValue ? ReferenceHeight.Value : 
                Math.Max( BuildingInnerHeight.Value, BuildingOuterHeight.Value);
            var externalPressureWindForceMax = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
                referenceHeight, calculateStructuralFactor: structuralFactorCalculator != null);
            var externalPressureWindForceMin = externalPressureWindForce.GetExternalPressureWindForceMinAt(
                referenceHeight, calculateStructuralFactor: structuralFactorCalculator != null);

            Result["e"] = structureData.EdgeDistance;
            Result["v_b,0_"] = buildingSite.FundamentalValueBasicWindVelocity;
            Result["c_dir_"] = directionalFactor?.GetFactor() ?? DirectionalFactor.DefaultDirectionalFactor;
            Result["v_b_"] = buildingSite.BasicWindVelocity;
            Result["c_r_(z_e_)"] = terrain.GetRoughnessFactorAt(referenceHeight);
            Result["c_0_(z_e_)"] = terrainOrography?.GetFactorAt(referenceHeight) ?? TerrainOrography.DefaultOrographyFactor;
            Result["v_m_(z_e_)"] = windLoadData.GetMeanWindVelocityAt(referenceHeight);
            Result["I_v_(z_e_)"] = windLoadData.GetTurbulenceIntensityAt(referenceHeight);
            Result["q_p_(z_e_)"] = windLoadData.GetPeakVelocityPressureAt(referenceHeight);
            Result["c_s_c_d_"] = structuralFactorCalculator?.GetStructuralFactor(structuralFactorCalculator != null) ?? StructuralFactorCalculator.DefaultStructuralFactor;
            SetPressureWindForces(externalPressureWindForceMax, isMax:true);
            SetPressureWindForces(externalPressureWindForceMin, isMax: false);

            return Result;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void SetPressureWindForces(IDictionary<Field, double> externalPressureWindForce, bool isMax)
        {
            string valueText = isMax ? "max" : "min";
            Result[$"w_e,F,{valueText}_"] = externalPressureWindForce.ContainsKey(Field.F) ? externalPressureWindForce[Field.F] : double.NaN;
            Result[$"w_e,G,{valueText}_"] = externalPressureWindForce.ContainsKey(Field.G) ? externalPressureWindForce[Field.G] : double.NaN;
            Result[$"w_e,H,{valueText}_"] = externalPressureWindForce.ContainsKey(Field.H) ? externalPressureWindForce[Field.H] : double.NaN;
            Result[$"w_e,I,{valueText}_"] = externalPressureWindForce.ContainsKey(Field.I) ? externalPressureWindForce[Field.I] : double.NaN;
            Result[$"w_e,J,{valueText}_"] = externalPressureWindForce.ContainsKey(Field.J) ? externalPressureWindForce[Field.J] : double.NaN;
        }

        private DuopitchRoof GetStructureData()
        {
            return new DuopitchRoof(BuildingLength.Value, BuildingWidth.Value,
                BuildingOuterHeight.Value, BuildingInnerHeight.Value, BuildingOrientation.Value);
        }

        private DuopitchRoofWindLoads GetDuopitchRoofWindLoads(IDuopitchRoof structure, IWindLoadData windLoadData)
        {
            return DuopitchRoofWindLoads.Create(structure, windLoadData);
        }

        #endregion // Private_Methods
    }
}
