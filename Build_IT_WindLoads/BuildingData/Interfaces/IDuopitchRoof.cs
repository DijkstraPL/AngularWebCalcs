
using Build_IT_WindLoads.BuildingData.Roofs;

namespace Build_IT_WindLoads.BuildingData.Interfaces
{
    public interface IDuopitchRoof : IStructure
    {
        #region Properties

        double Angle { get; }
        DuopitchRoof.Rotation CurrentRotation { get; }

        #endregion // Properties
    }
}