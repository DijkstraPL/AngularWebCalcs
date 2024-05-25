using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{
    public class CalculateNode : Node
    {
        private  CalculationParameter _calculationParameter;

        public void SetNext(Node nextNode)
        {
            _nextNode = nextNode;
        }
        public void SetCalculationParameter(CalculationParameter calculationParameter)
        {
            _calculationParameter = calculationParameter;
        }

        public override void Initialize(IEnumerable<ParameterMetadata> parameters)
        {
            if (!_calculationParameter.Initialized)
                _calculationParameter.Initialize(parameters);
            _nextNode?.Initialize(parameters);
        }

        public override bool IsValid()
        {
            return _calculationParameter is not null;
        }

        protected override CalculatedNode Calculate(IEnumerable<CalculatedNode> calculatedNodes)
        {
            var calculatedParameter = _calculationParameter.Calculate(calculatedNodes.SelectMany(cn => cn.CalculatedParameters));
            return new CalculatedNode(calculatedParameter);
        }
        protected override CalculatedNode CalculateNoLambda(IEnumerable<CalculatedNode> calculatedNodes)
        {
            var calculatedParameter = _calculationParameter.CalculateNoLambda(calculatedNodes.SelectMany(cn => cn.CalculatedParameters));
            return new CalculatedNode(calculatedParameter);
        }
    }
}
