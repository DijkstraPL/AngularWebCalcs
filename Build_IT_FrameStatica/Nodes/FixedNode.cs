using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.Reactions;
using Build_IT_Data.Geometry;

namespace Build_IT_FrameStatica.Nodes
{
    internal class FixedNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 0;

        #endregion // Properties

        #region Constructors

        public FixedNode(
            Point position,
            IValue normalForce = null,
            IValue shearForce = null,
            IValue bendingMoment = null)
            : base(position)
        {
            HorizontalForce = normalForce ?? new NormalForce();
            VerticalForce = shearForce ?? new ShearForce();
            BendingMoment = bendingMoment ?? new BendingMoment();
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        #endregion // Public_Methods
    }
}
