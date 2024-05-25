using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.Reactions;
using Build_IT_Data.Geometry;

namespace Build_IT_FrameStatica.Nodes
{
    internal class PinNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 2;

        #endregion // Properties

        #region Constructors

        public PinNode(
            Point position,
            IValue shearForce = null,
            IValue horizontalDeflection = null,
            IValue rotation = null)
            : base(position)
        {
            VerticalForce = shearForce ?? new ShearForce();
            HorizontalDeflection = horizontalDeflection ?? new HorizontalDeflection();
            LeftRotation = rotation ?? new Rotation();
            RightRotation = LeftRotation;
        }
        
        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            VerticalMovementNumber = currentCounter++;
        }

        #endregion // Public_Methods
    }
}
