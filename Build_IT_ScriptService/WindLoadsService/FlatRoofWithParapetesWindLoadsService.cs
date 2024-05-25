using Build_IT_Data.Calculators.Interfaces;
using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.WindLoadsCases.Roofs;
using System;
using System.Composition;

namespace Build_IT_ScriptService.WindLoadsService
{
    [Export("WindLoad-FlatRoofWithParapetes", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-4")]
    [ExportMetadata("Type", "WindLoad")]
    public class FlatRoofWithParapetesWindLoadsService : FlatRoofWindLoadsService
    {
        #region Properties
                
        public Property<double> ParapetHeight { get; } =
            new Property<double>("ParapetHeight",
                v => Convert.ToDouble(v));

        #endregion // Properties

        #region Constructors

        public FlatRoofWithParapetesWindLoadsService() : base()
        {
        }

        #endregion // Constructors

        #region Public_Methods

        #endregion // Public_Methods

        #region Protected_Methods

        protected override FlatRoof GetStructureData()
        {
            return new FlatRoofWithParapetes(
                BuildingLength.Value, 
                BuildingWidth.Value, 
                BuildingHeight.Value,
                ParapetHeight.Value,
                BuildingOrientation.HasValue ? BuildingOrientation.Value : FlatRoof.DefaultRotation);
        }

        protected override FlatRoofWindLoads GetFlatRoofWindLoads(IFlatRoofBuilding structure, IWindLoadData windLoadData)
        {
            return new FlatRoofWithParapetesWindLoads(structure, windLoadData, ParapetHeight.Value);
        }

        #endregion // Protected_Methods
    }
}
