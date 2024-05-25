using System.Collections.Generic;
using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class OperationNode : IMathMLNode
    {
        #region Fields
        
        private readonly string _value;

        #endregion // Fields

        #region Constructors
        
        public OperationNode(string value)
        {
            _value = GetFormattedValue(value);
        }

        private static string GetFormattedValue(string value)
        {
            if (value == "*")
                value = "&#xB7;";
            else if (value == "<")
                value = "&lt;";
            else if (value == ">")
                value = "&gt;";
            else if (value == "<=")
                value = "&#x2264;";
            else if (value == ">=")
                value = "&#x2265;";
            else if (value == "==")
                value = "=";
            else if (value == "&&")
                value = "AND";
            else if (value == "||")
                value = "OR";
            return value;
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            yield return new OperationMark(_value);
        }

        #endregion // Public_Methods
    }
}
