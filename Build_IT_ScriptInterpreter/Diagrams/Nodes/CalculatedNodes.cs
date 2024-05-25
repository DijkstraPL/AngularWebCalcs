using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{
    public class CalculatedNodes
    {
        private readonly IEnumerable<CalculatedNode> _calculatedNodes;

        public IEnumerable<CalculatedParameter> CalculatedParameters => _calculatedNodes.SelectMany(cn => cn.CalculatedParameters);

        public CalculatedParameter this[string parameterName] 
            => CalculatedParameters.FirstOrDefault(p => p.Name == parameterName);

        public CalculatedNodes(IEnumerable<CalculatedNode> calculatedNodes)
        {
            _calculatedNodes = calculatedNodes ?? throw new ArgumentNullException(nameof(calculatedNodes));
        }
    }
}
