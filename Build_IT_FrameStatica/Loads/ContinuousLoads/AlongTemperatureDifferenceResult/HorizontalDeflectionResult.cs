using Build_IT_FrameStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_FrameStatica.Loads.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Loads.ContinuousLoads.AlongTemperatureDifferenceResult
{
    internal class HorizontalDeflectionResult : DisplacementResultBase
    {
        #region Constructors

        public HorizontalDeflectionResult(IContinousLoad continousLoad)
            : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double GetValue(ISpan span, double distanceFromLeftSide, double currentLength)
            => span.Material.ThermalExpansionCoefficient
               * (ContinousLoad.StartPosition.Value - ContinousLoad.EndPosition.Value)
               * (distanceFromLeftSide - currentLength)*100;

        #endregion // Public_Methods
    }
}
