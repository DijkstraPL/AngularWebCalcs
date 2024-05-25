using Build_IT_NCalc.Enums;
using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes.Builders
{
    public class CalculateNodeBuilder : NodeBuilder, IParameterCalculateNodeBuilder
    {
        private readonly CalculateNode _calculateNode;

        internal CalculateNodeBuilder(List<ParameterMetadata> parameters) : base(parameters)
        {
            _calculateNode = new CalculateNode();
        }

        public NodeBuilder WithParameter(CalculationParameter calculationParameter)
        {
            _parameters.Add(new ParameterMetadata(calculationParameter.Name, calculationParameter.GetTypeCode()));
            _calculateNode.SetCalculationParameter(calculationParameter);
            return this;
        }

        public NodeBuilder WithParameter(string parameterName, string equation)
        {
            return WithParameter(new CalculationParameter(parameterName, equation));
        }

        public override Node Build()
        {
            if(_nextNodeBuilder is not null)
            {
                var node = _nextNodeBuilder.Build();
                _calculateNode.SetNext(node);
            }
            return _calculateNode;
        }
    }



    public interface IParameterCalculateNodeBuilder
    {
        NodeBuilder WithParameter(CalculationParameter calculationParameter);
        NodeBuilder WithParameter(string parameterName, string equation);
    }
}
