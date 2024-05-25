using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.Reactions;
using Build_IT_Data.Geometry;

namespace Build_IT_FrameStatica.Nodes
{
    internal class SupportedNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 1;

        #endregion // Properties

        #region Constructors
        
        public SupportedNode(
            Point position,
            IValue horizontalForce = null, 
            IValue verticalForce = null, 
            IValue rotation = null) 
            : base(position)
        {
            HorizontalForce = horizontalForce ?? new NormalForce();
            VerticalForce = verticalForce ?? new ShearForce();
            LeftRotation = rotation ?? new Rotation();
            RightRotation = LeftRotation;
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
        }

        #endregion // Public_Methods
    }
}
