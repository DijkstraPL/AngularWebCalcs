using Build_IT_FrameStatica.Loads.ContinuousLoads.Interfaces;
using Build_IT_FrameStatica.Loads.Interfaces;

namespace Build_IT_FrameStatica.Loads.ContinuousLoads.LoadResults
{
    internal abstract class ForceResultBase : ResultBase, IForceResult
    {
        #region Constructors

        protected ForceResultBase(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public abstract double GetValue(double distanceFromLoadStartPosition);

        #endregion // Public_Methods
    }
}
