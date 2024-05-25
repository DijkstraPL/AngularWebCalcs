using Build_IT_Data.Materials.Intefaces;
using Build_IT_Data.Sections.Interfaces;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Nodes.Interfaces;
using static Build_IT_FrameStatica.Frames.Interfaces.IFrame;

namespace Build_IT_FrameStatica.Spans.Interfaces
{
    public interface ISpan : ILengthProvider, INodesProvider, ILoadProvider
    {
        #region Properties

        short Number { get; set; }
        ISection Section { get; }
        IMaterial Material { get; }
        internal Forces LeftForces { get; }
        internal Forces RightForces { get; }
        internal Displacements LeftDisplacements { get; }
        internal Displacements RightDisplacements { get; } 

        bool IncludeSelfWeight { get;  }

        #endregion // Properties

        #region Public_Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Angle in degrees.</returns>
        double GetAngle();
        //(ISpan span1, INode hinge, ISpan span2) AddHinge(Side side);
        internal double GetLambdaX();
        internal double GetLambdaY();

        #endregion // Public_Methods
    }
}
