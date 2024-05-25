using Build_IT_NCalc.Enums;
using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes.Builders
{
    public class IfNodeBuilder : NodeBuilder, IConditionIfNodeBuilder, ITrueIfNodeBuilder, IFalseIfNodeBuilder
    {
        private readonly List<Action<IfNode>> _ifNodeActions = new();
        private NodeBuilder _trueBuilder;
        private NodeBuilder _falseBuilder;

        internal IfNodeBuilder(List<ParameterMetadata> parameters) : base(parameters)
        {
        }

        public ITrueIfNodeBuilder Condition(CalculationParameter calculationParameter)
        {
            _parameters.Add(new ParameterMetadata(calculationParameter.Name, calculationParameter.GetTypeCode()));
            _ifNodeActions.Add(ifNode => ifNode.SetIfStatement(calculationParameter));
            return this;
        }

        public IFalseIfNodeBuilder True(Func<NodeBuilder, NodeBuilder> node)
        {
            _trueBuilder = node(this);
            SetNextAction();
            return this;
        }
        public NodeBuilder False(Func<NodeBuilder, NodeBuilder> node)
        {
            _falseBuilder = node(this);
            SetNextAction();
            return this;
        }

        private void SetNextAction()
        {
            if (_trueBuilder is not null && _falseBuilder is not null)
                _ifNodeActions.Add(ifNode => ifNode.SetNext(_trueBuilder.Build(), _falseBuilder.Build()));
        }

        public override Node Build()
        {
            var ifNode = new IfNode();
            _ifNodeActions.ForEach(na => na(ifNode));
            return ifNode;
        }
    }

    public interface IFalseIfNodeBuilder
    {
        NodeBuilder False(Func<NodeBuilder, NodeBuilder> node);
    }

    public interface ITrueIfNodeBuilder
    {
        IFalseIfNodeBuilder True(Func<NodeBuilder, NodeBuilder> node);
    }

    public interface IConditionIfNodeBuilder
    {
        ITrueIfNodeBuilder Condition(CalculationParameter calculationParameter);
    }
}
