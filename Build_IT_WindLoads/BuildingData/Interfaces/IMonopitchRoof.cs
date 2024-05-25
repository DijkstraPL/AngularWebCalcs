
namespace Build_IT_WindLoads.BuildingData.Interfaces
{
    public interface IMonopitchRoof : IStructure
    {
        #region Properties
        
        double Angle { get; }
        MonopitchRoof.Rotation CurrentRotation { get; }

        #endregion // Properties
    }
}