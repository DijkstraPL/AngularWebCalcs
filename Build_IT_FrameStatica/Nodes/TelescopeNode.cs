using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.Reactions;
using Build_IT_Data.Geometry;

namespace Build_IT_FrameStatica.Nodes
{
    internal class TelescopeNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 1;

        #endregion // Properties

        #region Constructors
        
        public TelescopeNode(
            Point position,
            IResultValue normalForce = null, 
            IResultValue bendingMoment = null, 
            IResultValue verticalDeflection = null) 
            : base(position)
        {
            HorizontalForce = normalForce ?? new NormalForce();
            BendingMoment = bendingMoment ?? new BendingMoment();
            VerticalDeflection = verticalDeflection ?? new VerticalDeflection();
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            VerticalMovementNumber = currentCounter++;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        #endregion // Public_Methods
    }
}
