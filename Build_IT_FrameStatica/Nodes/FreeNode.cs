using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_Data.Geometry;

namespace Build_IT_FrameStatica.Nodes
{
    internal class FreeNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 3;

        #endregion // Properties

        #region Constructors 

        public FreeNode(
            Point position,
            IValue horizontalDeflection = null,
            IValue verticalDeflection = null,
            IValue rotation = null)
            : base(position)
        {
            HorizontalDeflection = horizontalDeflection ?? new HorizontalDeflection();
            VerticalDeflection = verticalDeflection ?? new VerticalDeflection();
            LeftRotation = rotation ?? new Rotation();
            RightRotation = LeftRotation;
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
        }

        #endregion // Public_Methods
    }
}
