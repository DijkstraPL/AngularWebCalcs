using Build_IT_Data.Calculators;
using Build_IT_Data.Calculators.Interfaces;
using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.BuildingData.Walls;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.TerrainOrographies;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.WindLoadsCases;
using System;
using System.Collections.Generic;
using System.Composition;

namespace Build_IT_ScriptService.WindLoadsService
{
    [Export("WindLoad-Walls", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-4")]
    [ExportMetadata("Type", "WindLoad")]
    public class WallsWindLoadsService : WindLoadBaseService
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
        public Property<EqualHeightWalls.Rotation> BuildingOrientation { get; } =
            new Property<EqualHeightWalls.Rotation>("BuildingOrientation",
                v => Enum.Parse<EqualHeightWalls.Rotation>(v.ToString()), required: false);


        #endregion // Properties

        #region Constructors

        public WallsWindLoadsService()
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
                { "w_e,A_", null },
                { "w_e,B_", null },
                { "w_e,C_", null },
                { "w_e,D_", null },
                { "w_e,E_", null },
            });
        }

        #endregion // Constructors

        #region Public_Methods

        public override IResult Calculate()
        {
            EqualHeightWalls structureData = GetStructureData();
            HeightDisplacement heightDisplacement = GetHeightDisplacement(structureData);
            TerrainOrography terrainOrography = GetTerrainOrography();
            Terrain terrain = GetTerrainCategory(heightDisplacement, terrainOrography);
            DirectionalFactor directionalFactor = GetDirectionalFactor();
            BuildingSite buildingSite = GetBuildingSite(terrain, directionalFactor);
            ReferenceHeightDueToNeighbouringStructures referenceHeightDueToNeighbouringStructures
                = GetReferenceHeightDueToNeighbouringStructures();
            WindLoadData windLoadData = GetWindLoadData(buildingSite, structureData, referenceHeightDueToNeighbouringStructures);
            WallsWindLoads wallsWindLoads = GetWallsWindLoads(structureData, windLoadData);
            StructuralFactorCalculator structuralFactorCalculator =
                GetStructuralFactorCalculator(structureData, terrain, windLoadData);
            ExternalPressureWindForce externalPressureWindForce =
                GetExternalPressureWindForce(windLoadData, wallsWindLoads, structuralFactorCalculator);

            var referenceHeight = ReferenceHeight.HasValue ? ReferenceHeight.Value : BuildingHeight.Value;
            var externalPressureWindForceMax = externalPressureWindForce.GetExternalPressureWindForceMaxAt(
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
            Result["w_e,A_"] = externalPressureWindForceMax.ContainsKey(Field.A) ? externalPressureWindForceMax[Field.A] : double.NaN;
            Result["w_e,B_"] = externalPressureWindForceMax.ContainsKey(Field.B) ? externalPressureWindForceMax[Field.B] : double.NaN;
            Result["w_e,C_"] = externalPressureWindForceMax.ContainsKey(Field.C) ? externalPressureWindForceMax[Field.C] : double.NaN;
            Result["w_e,D_"] = externalPressureWindForceMax.ContainsKey(Field.D) ? externalPressureWindForceMax[Field.D] : double.NaN;
            Result["w_e,E_"] = externalPressureWindForceMax.ContainsKey(Field.E) ? externalPressureWindForceMax[Field.E] : double.NaN;
 
            return Result;
        }


        #endregion // Public_Methods

        #region Private_Methods

        private EqualHeightWalls GetStructureData()
        {
            return new EqualHeightWalls(BuildingLength.Value, BuildingWidth.Value, BuildingHeight.Value,
                BuildingOrientation.HasValue ? BuildingOrientation.Value : EqualHeightWalls.DefaultRotation);
        }


        private WallsWindLoads GetWallsWindLoads(IStructure structure, IWindLoadData windLoadData)
        {
            return new WallsWindLoads(structure, windLoadData);
        }
        
        #endregion // Private_Methods
    }
}
