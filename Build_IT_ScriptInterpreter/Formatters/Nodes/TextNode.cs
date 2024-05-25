using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class TextNode : IMathMLNode
    {
        #region Fields

        private readonly string _value;

        #endregion // Fields

        #region Constructors

        public TextNode(string value)
        {
            _value = value;
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            yield return new TextMark(_value);
        }
        
        #endregion // Public_Methods
    }
}
