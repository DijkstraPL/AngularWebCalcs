using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{
    public class DataNode : Node
    {
        private readonly List<InputParameter> _inputParameters = new List<InputParameter>();

        public override void Initialize(IEnumerable<ParameterMetadata> parameters)
        {
            _nextNode?.Initialize(parameters);
        }

        public void AddInputParameter(InputParameter inputParameter)
        {
            _inputParameters.Add(inputParameter);
        }

        public void SetNext(Node nextNode)
        {
            _nextNode = nextNode;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override CalculatedNode Calculate(IEnumerable<CalculatedNode> calculatedNodes)
        {
            List<CalculatedParameter> calculatedParameters = new();
            foreach (var inputParameter in _inputParameters)
            {
                var calculatedParameter = inputParameter.Calculate();
                calculatedParameters.Add(calculatedParameter);
            }
            return new CalculatedNode(calculatedParameters.ToArray());
        }
        protected override CalculatedNode CalculateNoLambda(IEnumerable<CalculatedNode> calculatedNodes)
        {
            List<CalculatedParameter> calculatedParameters = new();
            foreach (var inputParameter in _inputParameters)
            {
                var calculatedParameter = inputParameter.CalculateNoLambda();
                calculatedParameters.Add(calculatedParameter);
            }
            return new CalculatedNode(calculatedParameters.ToArray());
        }

        public override void SetValue(string parameterName, object value)
        {
            var parameter = _inputParameters.FirstOrDefault(p => p.Name == parameterName);
            if (parameter is not null)
                parameter.SetValue(value);

            _nextNode?.SetValue(parameterName, value);
        }
    }
}
