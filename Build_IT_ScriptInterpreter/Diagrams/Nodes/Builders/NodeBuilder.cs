using Build_IT_NCalc.Enums;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes.Builders
{
    public abstract class NodeBuilder
    {
        protected NodeBuilder _nextNodeBuilder;

        protected List<ParameterMetadata> _parameters;

        internal NodeBuilder(List<ParameterMetadata> parameters)
        {
            _parameters = parameters ?? throw new System.ArgumentNullException(nameof(parameters));
        }

        public IParameterDataNodeBuilder Data
        {
            get
            {
                var dataBuilder = new DataNodeBuilder(_parameters);
                _nextNodeBuilder = dataBuilder;
                return dataBuilder;
            }
        }
        public IfNodeBuilder If
        {
            get
            {
                var ifNodeBuilder = new IfNodeBuilder(_parameters);
                _nextNodeBuilder = ifNodeBuilder;
                return ifNodeBuilder;
            }
        }
        public IParameterCalculateNodeBuilder Equation
        {
            get
            {
                var calculateNodeBuilder = new CalculateNodeBuilder(_parameters);
                _nextNodeBuilder = calculateNodeBuilder;
                return calculateNodeBuilder;
            }
        }

        public abstract Node Build();
    }
}
