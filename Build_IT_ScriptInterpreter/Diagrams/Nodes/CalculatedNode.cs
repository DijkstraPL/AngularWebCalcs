using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{
    public class CalculatedNode
    {
        public IEnumerable<CalculatedParameter> CalculatedParameters { get; }

        public CalculatedNode(params CalculatedParameter[] calculatedParameters)
        {
            CalculatedParameters = calculatedParameters ?? throw new ArgumentNullException(nameof(calculatedParameters));
        }
    }
}
