using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_Data.Geometry;

namespace Build_IT_FrameStatica.Nodes
{
    internal class Hinge : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 4;

        #endregion // Properties

        #region Constructors

        public Hinge(
            Point position,
            IResultValue horizontalDeflection = null,
            IResultValue verticalDeflection = null, 
            IResultValue leftRotation = null, 
            IResultValue rightRotation = null)
            : base(position)
        {
            HorizontalDeflection = horizontalDeflection ?? new VerticalDeflection();
            VerticalDeflection = verticalDeflection ?? new VerticalDeflection();
            LeftRotation = leftRotation ?? new Rotation();
            RightRotation = rightRotation ?? new Rotation();
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = currentCounter++;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
        }

        #endregion // Public_Methods
    }
}
