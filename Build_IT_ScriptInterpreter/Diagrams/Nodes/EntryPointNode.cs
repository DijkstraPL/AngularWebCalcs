using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{
    public class EntryPointNode : Node
    {
        public override void Initialize(IEnumerable<ParameterMetadata> parameters)
        {
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override CalculatedNode Calculate(IEnumerable<CalculatedNode> calculatedNodes)
        {
            return null;
        }
        protected override CalculatedNode CalculateNoLambda(IEnumerable<CalculatedNode> calculatedNodes)
        {
            return null;
        }

        public void SetNext(Node nextNode)
        {
            _nextNode = nextNode;
        }
    }
}
