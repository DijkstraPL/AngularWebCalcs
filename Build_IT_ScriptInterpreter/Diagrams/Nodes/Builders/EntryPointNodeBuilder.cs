using Build_IT_NCalc.Enums;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes.Builders
{
    public class EntryPointNodeBuilder : NodeBuilder
    {
        public EntryPointNodeBuilder() : base(new List<ParameterMetadata>())
        {

        }

        public override Node Build()
        {
            return _nextNodeBuilder.Build();
        }
        public  Node BuildInitialized()
        {
            var node = _nextNodeBuilder.Build();
            node.Initialize(_parameters);

            return node;
        }
    }
}
