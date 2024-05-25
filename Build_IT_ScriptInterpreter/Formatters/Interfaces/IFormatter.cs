using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Interfaces
{
    public interface IFormatter
    {
        #region Public_Methods

        IMathMLNode GetMathML(string value);
        IMathMLNode GetMathMLWithValues(string value, IDictionary<string, (string, string)> parameters);

        #endregion // Public_Methods
    }
}
