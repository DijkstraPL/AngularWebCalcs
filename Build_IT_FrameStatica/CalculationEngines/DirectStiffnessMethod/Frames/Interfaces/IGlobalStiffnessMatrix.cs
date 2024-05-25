using Build_IT_CommonTools.MatrixMath.Wrappers;

namespace Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Frames.Interfaces
{
    public interface IGlobalStiffnessMatrix
    {
        #region Properties

        MatrixAdapter Matrix { get; }
        MatrixAdapter InversedMatrix { get; }

        #endregion // Properties

        #region Public_Methods

        void Calculate();

        #endregion // Public_Methods
    }
}
