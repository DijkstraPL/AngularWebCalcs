using Build_IT_WindLoads.BuildingData.Roofs;

namespace Build_IT_WindLoads.BuildingData.Interfaces
{
    public interface IHippedRoof : IStructure
    {
        #region Properties

        HippedRoof.Rotation CurrentRotation { get; }
        double Angle0 { get; }
        double Angle90 { get; }

        #endregion // Properties
    }
}