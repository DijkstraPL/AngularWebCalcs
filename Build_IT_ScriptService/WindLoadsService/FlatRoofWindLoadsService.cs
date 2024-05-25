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
    [Export("WindLoad-FlatRoof", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-4")]
    [ExportMetadata("Type", "WindLoad")]
    public class FlatRoofWindLoadsService : WindLoadBaseService
    {
        #region Properties

        public Property<double> BuildingLength { get; } =
            new Property<double>("BuildingLength",
                v => Convert.ToDouble(v));
        public Property<double> BuildingWidth { get; } =
            new Property<double>("BuildingWidth",
                v => Convert.ToDouble(v));
        public Property<double> BuildingHeight { get; } =
            new Property<double>("BuildingHeight",
                v => Convert.ToDouble(v));

        public Property<double> ReferenceHeight { get; } =
            new Property<double>("ReferenceHeight",
                v => Convert.ToDouble(v));

        /// <summary>
        /// Building side
        /// </summary>
        public Property<FlatRoof.Rotation> BuildingOrientation { get; } =
            new Property<FlatRoof.Rotation>("BuildingOrientation",
                v => Enum.Parse<FlatRoof.Rotation>(v.ToString()), required: false);

        #endregion // Properties
        #region Constructors

        public FlatRoofWindLoadsService()
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
                { "w_e,F,min_", null },
                { "w_e,G,min_", null },
                { "w_e,H,min_", null },
                { "w_e,I,min_", null },
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            FlatRoof structureData = GetStructureData();
            HeightDisplacement heightDisplacement = GetHeightDisplacement(structureData);
            TerrainOrography terrainOrography = GetTerrainOrography();
            Terrain terrain = GetTerrainCategory(heightDisplacement, terrainOrography);
            DirectionalFactor directionalFactor = GetDirectionalFactor();
            BuildingSite buildingSite = GetBuildingSite(terrain, directionalFactor);
            ReferenceHeightDueToNeighbouringStructures referenceHeightDueToNeighbouringStructures
                = GetReferenceHeightDueToNeighbouringStructures();
            WindLoadData windLoadData = GetWindLoadData(buildingSite, structureData, referenceHeightDueToNeighbouringStructures);
            FlatRoofWindLoads flatRoofWindLoads = GetFlatRoofWindLoads(structureData, windLoadData);
            StructuralFactorCalculator structuralFactorCalculator =
                GetStructuralFactorCalculator(structureData, terrain, windLoadData);
            ExternalPressureWindForce externalPressureWindForce =
                GetExternalPressureWindForce(windLoadData, flatRoofWindLoads, structuralFactorCalculator);

            var referenceHeight = ReferenceHeight.HasValue ? ReferenceHeight.Value : BuildingHeight.Value;
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
            Result["w_e,F,max_"] = externalPressureWindForceMax.ContainsKey(Field.F) ? externalPressureWindForceMax[Field.F] : double.NaN;
            Result["w_e,G,max_"] = externalPressureWindForceMax.ContainsKey(Field.G) ? externalPressureWindForceMax[Field.G] : double.NaN;
            Result["w_e,H,max_"] = externalPressureWindForceMax.ContainsKey(Field.H) ? externalPressureWindForceMax[Field.H] : double.NaN;
            Result["w_e,I,max_"] = externalPressureWindForceMax.ContainsKey(Field.I) ? externalPressureWindForceMax[Field.I] : double.NaN;
            Result["w_e,F,min_"] = externalPressureWindForceMin.ContainsKey(Field.F) ? externalPressureWindForceMin[Field.F] : double.NaN;
            Result["w_e,G,min_"] = externalPressureWindForceMin.ContainsKey(Field.G) ? externalPressureWindForceMin[Field.G] : double.NaN;
            Result["w_e,H,min_"] = externalPressureWindForceMin.ContainsKey(Field.H) ? externalPressureWindForceMin[Field.H] : double.NaN;
            Result["w_e,I,min_"] = externalPressureWindForceMin.ContainsKey(Field.I) ? externalPressureWindForceMin[Field.I] : double.NaN;

            return Result;
        }

        #endregion // Public_Methods

        #region Protected_Methods

        protected virtual FlatRoof GetStructureData()
        {
            return new FlatRoof(BuildingLength.Value, BuildingWidth.Value, BuildingHeight.Value,
                BuildingOrientation.HasValue ? BuildingOrientation.Value : FlatRoof.DefaultRotation);
        }

        protected virtual FlatRoofWindLoads GetFlatRoofWindLoads(IFlatRoofBuilding structure, IWindLoadData windLoadData)
        {
            return new FlatRoofWindLoads(structure, windLoadData);
        }

        #endregion // Protected_Methods
    }
}
