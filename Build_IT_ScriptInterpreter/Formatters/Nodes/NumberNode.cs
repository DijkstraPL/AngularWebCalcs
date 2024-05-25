using System.Collections.Generic;
using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class NumberNode : IMathMLNode
    {
        #region Fields
        
        private readonly string _value;

        #endregion // Fields

        #region Constructors
        
        public NumberNode(string value)
        {
            _value = value;
        }

        public IEnumerable<Mark> GetMarks()
        {
            yield return new NumberMark(_value);
        }

        #endregion // Constructors

        #region Public_Methods
        
        #endregion // Public_Methods
    }
}
