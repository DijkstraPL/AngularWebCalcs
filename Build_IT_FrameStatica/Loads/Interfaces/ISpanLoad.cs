using Build_IT_FrameStatica.Spans.Interfaces;

namespace Build_IT_FrameStatica.Loads.Interfaces
{
    public interface ISpanLoad : ILoadWithPosition, INodeLoad
    {
        #region Public_Methods

        double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode);
        double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode);
        double CalculateSpanLoadBendingMomentMember(ISpan span, bool leftNode);

        #endregion // Public_Methods
    }
}
