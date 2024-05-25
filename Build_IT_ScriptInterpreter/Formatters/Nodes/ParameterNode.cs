using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class ParameterNode : IMathMLNode
    {
        #region Fields

        private readonly string _value;
        private IMathMLContainer _rowNode;

        #endregion // Fields

        #region Constructors

        public ParameterNode(string value)
        {
            _value = value;

            if (_value.Contains("_"))
                FormatParameter();
        }

        #endregion // Constructors

        #region Public_Methods


        public IEnumerable<Mark> GetMarks()
        {
            if (_rowNode == null)
                yield return new IdentifierMark(_value);
            else
            {
                foreach (var mark in _rowNode.GetMarks())
                    yield return mark;
            }
        }
        
        #endregion // Public_Methods

        #region Private_Methods

        private void FormatParameter()
        {
            bool inSubscript = false;

            string value = string.Empty;

            int index = -1;
            foreach (var @char in _value)
            {
                index++;
                if (@char == '_')
                {
                    inSubscript = !inSubscript;

                    if (!inSubscript)
                    {
                        var subscript = new SubscriptNode($"[{value.Split('_')[0]}]_[{value.Split('_')[1]}]");
                        var rest = _value.Substring(index);
                        ParameterNode nextNode = null;
                        if (rest != "_")
                            nextNode = new ParameterNode(_value.Substring(index + 1));

                        _rowNode = new RowNode();
                        if (subscript != null)
                            _rowNode.Add(subscript);
                        if (nextNode != null)
                            _rowNode.Add(nextNode);
                        return;
                    }
                }

                value += @char;
            }
        }

        #endregion // Private_Methods
    }
}
