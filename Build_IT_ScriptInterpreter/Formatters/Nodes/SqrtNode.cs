using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class SqrtNode : IMathMLNode
    {
        #region Fields
        
        private readonly string _value;
        private readonly IMathMLContainer _valueRow;

        #endregion // Fields

        #region Constructors
        
        public SqrtNode(string value)
        {
            _value = value;
            var valueFormatter = new MathMLFormatter();
            var valueNodes = valueFormatter.Format(_value);
            _valueRow = new RowNode();
            _valueRow.AddRange(valueNodes);
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            yield return new SqrtMark(_valueRow.GetMarks());
        }
        
        #endregion // Public_Methods
    }
}
