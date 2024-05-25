using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.DynamicCharacteristics.Enums;
using Build_IT_WindLoads.Factors;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.TerrainOrographies;
using Build_IT_WindLoads.TerrainOrographies.Enums;
using Build_IT_WindLoads.Terrains;
using Build_IT_WindLoads.Terrains.Enums;
using Build_IT_WindLoads.Terrains.Interfaces;
using Build_IT_WindLoads.WindLoadsCases;
using Build_IT_WindLoads.WindLoadsCases.Interfaces;
using System;

namespace Build_IT_ScriptService.WindLoadsService
{
    public abstract class WindLoadBaseService : ServiceBase
    {
        #region Properties

        public Property<TerrainType> TerrainType { get; } =
            new Property<TerrainType>("TerrainType",
                v => Enum.Parse<TerrainType>(v.ToString()));
        public Property<double> HeightAboveSeaLevel { get; } =
            new Property<double>("HeightAboveSeaLevel",
                v => Convert.ToDouble(v));
        public Property<WindZone> WindZone { get; } =
            new Property<WindZone>("WindZone",
                v => Enum.Parse<WindZone>(v.ToString()));

        // HeightDisplacement
        public Property<double> HorizontalDistanceToObstruction { get; } =
            new Property<double>("HorizontalDistanceToObstruction",
                v => Convert.ToDouble(v), required: false);
        public Property<double> ObstructionHeight { get; } =
            new Property<double>("ObstructionHeight",
                v => Convert.ToDouble(v), required: false); // h_ave_
        // -- HeightDisplacement

        // Orography
        public Property<Orographies> Orography { get; } =
            new Property<Orographies>("Orography",
                v => Enum.Parse<Orographies>(v.ToString()), required: false);
        public Property<double> ActualLengthUpwindSlope { get; } =
            new Property<double>("ActualLengthUpwindSlope",
                v => Convert.ToDouble(v), required: false);
        public Property<double> ActualLengthDownwindSlope { get; } =
            new Property<double>("ActualLengthDownwindSlope",
                v => Convert.ToDouble(v), required: false);
        public Property<double> EffectiveFeatureHeight { get; } =
            new Property<double>("EffectiveFeatureHeight",
                v => Convert.ToDouble(v), required: false);
        public Property<double> HorizontalDistanceFromCrestTop { get; } =
            new Property<double>("HorizontalDistanceFromCrestTop",
                v => Convert.ToDouble(v), required: false);
        // -- Orography

        // DirectionalFactor
        public Property<double> WindDirection { get; } =
            new Property<double>("WindDirection",
                v => Convert.ToDouble(v), required: false);
        // -- DirectionalFactor

        // ReferenceHeightDueToNeighbouringStructures
        public Property<double> HighBuildingWidth { get; } =
            new Property<double>("HighBuildingWidth",
                v => Convert.ToDouble(v), required: false);
        public Property<double> HighBuildingLength { get; } =
            new Property<double>("HighBuildingLength",
                v => Convert.ToDouble(v), required: false);
        public Property<double> HighBuildingHeight { get; } =
            new Property<double>("HighBuildingHeight",
                v => Convert.ToDouble(v), required: false);
        public Property<double> DistanceToBuilding { get; } =
            new Property<double>("DistanceToBuilding",
                v => Convert.ToDouble(v), required: false);
        // -- ReferenceHeightDueToNeighbouringStructures

        // StructuralFactorCalculator
        public Property<StructuralType> StructuralType { get; } =
            new Property<StructuralType>("StructuralType",
                v => Enum.Parse<StructuralType>(v.ToString()), required: false);
        // -- StructuralFactorCalculator


        #endregion // Properties

        #region Protected_Methods

        /// <summary>
        /// Could be calculated in Terrain category IV
        /// </summary>
        /// <param name="structureData"></param>
        /// <returns></returns>
        protected HeightDisplacement GetHeightDisplacement(IStructure structureData)
        {
            if (HorizontalDistanceToObstruction.HasValue && TerrainType.Value == Build_IT_WindLoads.Terrains.Enums.TerrainType.Terrain_IV)
                return new HeightDisplacement(structureData,
                    HorizontalDistanceToObstruction.Value,
                    ObstructionHeight.HasValue ? ObstructionHeight.Value : HeightDisplacement.DefaultObstructionHeight);
            return null;
        }

        protected TerrainOrography GetTerrainOrography()
        {
            if (!Orography.HasValue)
                return null;

            if (Orography.Value == Orographies.CliffEscarpment)
                return new CliffEscarpmentOrography(
                   ActualLengthUpwindSlope.HasValue ? ActualLengthUpwindSlope.Value : default,
                   EffectiveFeatureHeight.HasValue ? EffectiveFeatureHeight.Value : default,
                   HorizontalDistanceFromCrestTop.HasValue ? HorizontalDistanceFromCrestTop.Value : default);
            else if (Orography.Value == Orographies.HillRidge)
                return new HillRidgeOrography(
                   ActualLengthUpwindSlope.HasValue ? ActualLengthUpwindSlope.Value : default,
                   ActualLengthDownwindSlope.HasValue ? ActualLengthDownwindSlope.Value : default,
                   EffectiveFeatureHeight.HasValue ? EffectiveFeatureHeight.Value : default,
                   HorizontalDistanceFromCrestTop.HasValue ? HorizontalDistanceFromCrestTop.Value : default);

            throw new ArgumentException(nameof(Orography));
        }

        protected Terrain GetTerrainCategory(HeightDisplacement heightDisplacement, TerrainOrography terrainOrography)
        {
            return Terrain.Create(TerrainType.Value, heightDisplacement, terrainOrography);
        }

        protected DirectionalFactor GetDirectionalFactor()
        {
            if (WindDirection.HasValue)
                return new DirectionalFactor(WindZone.Value, WindDirection.Value);
            return null;
        }

        protected BuildingSite GetBuildingSite(Terrain terrain, DirectionalFactor directionalFactor)
        {
            return new BuildingSite(HeightAboveSeaLevel.Value, WindZone.Value,
                terrain, seasonalFactor: null, directionalFactor);
        }

        protected ReferenceHeightDueToNeighbouringStructures GetReferenceHeightDueToNeighbouringStructures()
        {
            if (HighBuildingWidth.HasValue && HighBuildingLength.HasValue &&
                HighBuildingHeight.HasValue && DistanceToBuilding.HasValue)
                return new ReferenceHeightDueToNeighbouringStructures(
                    HighBuildingWidth.Value,
                    HighBuildingLength.Value,
                    HighBuildingHeight.Value,
                    DistanceToBuilding.Value);
            return null;
        }

        protected WindLoadData GetWindLoadData(IBuildingSite buildingSite, IStructure structure,
            ReferenceHeightDueToNeighbouringStructures referenceHeightDueToNeighbouringStructures)
        {
            return new WindLoadData(buildingSite, structure,
                WindLoadData.DefaultHeightStrip, WindLoadData.AllowCustomReferenceHeight,
                referenceHeightDueToNeighbouringStructures
                );
        }

        protected StructuralFactorCalculator GetStructuralFactorCalculator(
            IStructure structure, ITerrain terrain, IWindLoadData windLoadData)
        {
            if (StructuralType.HasValue)
                return new StructuralFactorCalculator(
                    structure, terrain, windLoadData, StructuralType.Value);
            return null;
        }

        protected ExternalPressureWindForce GetExternalPressureWindForce(
            IWindLoadData windLoadData, IWindLoadCase wallsWindLoads, IStructuralFactorCalculator structuralFactorCalculator)
        {
            return new ExternalPressureWindForce(
                    windLoadData,
                    wallsWindLoads,
                    structuralFactorCalculator);
        }

        #endregion // Protected_Methods
    }
}
