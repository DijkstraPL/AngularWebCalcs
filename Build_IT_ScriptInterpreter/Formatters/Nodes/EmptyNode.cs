using System.Collections.Generic;
using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;

namespace Build_IT_ScriptInterpreter.Formatters
{
    internal class EmptyNode : IMathMLNode
    {
        #region Fields

        private readonly string _value;

        #endregion // Fields

        #region Constructors

        public EmptyNode(string value)
        {
            _value = value;
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            yield return new Mark(null);
        }

        #endregion // Public_Methods
    }
}