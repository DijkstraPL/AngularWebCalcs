using Build_IT_ScriptInterpreter.Expressions.Parameters.Interfaces;
using System;
using System.Composition;

namespace Build_IT_ScriptInterpreter.Expressions.Parameters
{
    [Export(typeof(ICustomParameter))]
    public class PiParameter : ICustomParameter<double>
    {
        #region Properties

        public string[] Names { get; } = { "π", "Pi" };
        public double Value { get; } = Math.PI;
        
        object ICustomParameter.Value => Value;
        
        #endregion // Properties
    }
}
