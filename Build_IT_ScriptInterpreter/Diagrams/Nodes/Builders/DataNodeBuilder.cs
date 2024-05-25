using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes.Builders
{
    public class DataNodeBuilder : NodeBuilder, IParameterDataNodeBuilder
    {
        private readonly DataNode _dataNode;

        internal DataNodeBuilder(List<ParameterMetadata> parameters) : base(parameters)
        {
            _dataNode = new DataNode();
        }

        public DataNodeBuilder With(InputParameter inputParameter)
        {
            _parameters.Add(new ParameterMetadata(inputParameter.Name, inputParameter.GetTypeCode()));
            _dataNode.AddInputParameter(inputParameter);
            return this;
        }

        public DataNodeBuilder With(string parameterName)
        {
            return With(new InputParameter<ValueUnit>(parameterName));
        }

        public override Node Build()
        {
            if (_nextNodeBuilder is not null)
            {
                var node = _nextNodeBuilder.Build();
                _dataNode.SetNext(node);
            }
            return _dataNode;
        }
    }

    public interface IParameterDataNodeBuilder
    {
        DataNodeBuilder With(InputParameter inputParameter);
        DataNodeBuilder With(string parameterName);
    }
}
