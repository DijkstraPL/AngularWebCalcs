using Build_IT_Data.Calculators.Interfaces;
using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.WindLoadsCases.Roofs;
using System;
using System.Composition;

namespace Build_IT_ScriptService.WindLoadsService
{
    [Export("WindLoad-FlatRoofWithCurvedEaves", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-4")]
    [ExportMetadata("Type", "WindLoad")]
    public class FlatRoofWithCurvedEavesWindLoadsService : FlatRoofWindLoadsService
    {
        #region Properties
                
        public Property<double> Curvature { get; } =
            new Property<double>("Curvature",
                v => Convert.ToDouble(v));

        #endregion // Properties

        #region Constructors

        public FlatRoofWithCurvedEavesWindLoadsService() : base()
        {
        }

        #endregion // Constructors

        #region Public_Methods

        #endregion // Public_Methods

        #region Protected_Methods
               
        protected override FlatRoofWindLoads GetFlatRoofWindLoads(
            IFlatRoofBuilding structure, IWindLoadData windLoadData)
        {
            return new FlatRoofWithCurvedEavesWindLoads(structure, windLoadData, Curvature.Value);
        }

        #endregion // Protected_Methods
    }
}
