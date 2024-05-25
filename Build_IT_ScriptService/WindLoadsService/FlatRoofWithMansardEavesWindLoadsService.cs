using Build_IT_Data.Calculators.Interfaces;
using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.WindLoadsCases.Roofs;
using System;
using System.Composition;

namespace Build_IT_ScriptService.WindLoadsService
{
    [Export("WindLoad-FlatRoofWithMansardEaves", typeof(ICalculator))]
    [Export(typeof(ICalculator))]
    [ExportMetadata("Document", "PN-EN 1991-1-4")]
    [ExportMetadata("Type", "WindLoad")]
    public class FlatRoofWithMansardEavesWindLoadsService : FlatRoofWindLoadsService
    {
        #region Properties
                
        public Property<double> Angle { get; } =
            new Property<double>("Angle",
                v => Convert.ToDouble(v));

        #endregion // Properties

        #region Constructors

        public FlatRoofWithMansardEavesWindLoadsService() : base()
        {
        }

        #endregion // Constructors

        #region Public_Methods

        #endregion // Public_Methods

        #region Protected_Methods
               
        protected override FlatRoofWindLoads GetFlatRoofWindLoads(IFlatRoofBuilding structure, IWindLoadData windLoadData)
        {
            return new FlatRoofWithMansardEavesWindLoads(structure, windLoadData, Angle.Value);
        }

        #endregion // Protected_Methods
    }
}
