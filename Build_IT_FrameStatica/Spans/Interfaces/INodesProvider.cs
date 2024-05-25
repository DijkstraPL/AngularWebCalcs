using Build_IT_FrameStatica.Nodes.Interfaces;

namespace Build_IT_FrameStatica.Spans.Interfaces
{
    public interface INodesProvider
    {
        #region Properties

        INode LeftNode { get; }
        INode RightNode { get; }

        #endregion // Properties
    }
}
