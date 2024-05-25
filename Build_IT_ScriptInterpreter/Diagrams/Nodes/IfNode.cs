using Build_IT_NCalc.Enums;
using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{
    public class IfNode : Node
    {
        private  CalculationParameter _ifStatement;
        private Node _trueNode;
        private Node _falseNode;

        public override void Initialize(IEnumerable<ParameterMetadata> parameters)
        {
            if (!IsValid())
                return;
            if (!_ifStatement.Initialized)
                _ifStatement.Initialize(parameters);
            _trueNode?.Initialize(parameters);
            _falseNode?.Initialize(parameters);
        }

        public void SetIfStatement(CalculationParameter calculationParameter)
        {
            _ifStatement = calculationParameter;
        }

        public void SetNext(Node trueNode, Node falseNode)
        {
            _trueNode = trueNode;
            _falseNode = falseNode;
        }

        public override bool IsValid()
        {
            return _ifStatement is not null;
        }

        public override void SetValue(string parameterName, object value)
        {
            _trueNode?.SetValue(parameterName, value);
            _falseNode?.SetValue(parameterName, value);

            _nextNode?.SetValue(parameterName, value);
        }

        protected override CalculatedNode Calculate(IEnumerable<CalculatedNode> calculatedNodes)
        {
            var result = _ifStatement.Calculate(calculatedNodes.SelectMany(cn => cn.CalculatedParameters));
            bool? booleanResult = result.Value as bool?;
            if (booleanResult is null || !booleanResult.HasValue)
                throw new ArithmeticException("Couldn't calculate expression as boolean.");
            _nextNode = booleanResult.Value ? _trueNode : _falseNode;
            return new CalculatedNode(result);
        }

        protected override CalculatedNode CalculateNoLambda(IEnumerable<CalculatedNode> calculatedNodes)
        {
            var result = _ifStatement.CalculateNoLambda(calculatedNodes.SelectMany(cn => cn.CalculatedParameters));
            bool? booleanResult = result.Value as bool?;
            if (booleanResult is null || !booleanResult.HasValue)
                throw new ArithmeticException("Couldn't calculate expression as boolean.");
            _nextNode = booleanResult.Value ? _trueNode : _falseNode;
            return new CalculatedNode(result);
        }
    }
}
