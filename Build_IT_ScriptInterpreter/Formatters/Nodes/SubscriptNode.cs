using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class SubscriptNode : IMathMLNode
    {
        #region Fields

        private readonly IMathMLContainer _valueRow;
        private readonly IMathMLContainer _subscriptRow;

        #endregion // Fields

        #region Constructors
        
        public SubscriptNode(string value)
        {
            _valueRow = new RowNode();
            _subscriptRow = new RowNode();

            var valueFormatter = new MathMLFormatter();
            var valueNodes = valueFormatter.Format(value.Split('_')[0]);
            _valueRow.AddRange(valueNodes);

            var subscriptFormatter = new MathMLFormatter();
            var subscriptNodes = subscriptFormatter.Format(value.Split('_')[1]);
            _subscriptRow.AddRange(subscriptNodes);
        }

        #endregion // Constructors

        #region Public_Methods
        
        public IEnumerable<Mark> GetMarks()
        {
            yield return new SubscriptMark(
                _valueRow.GetMarks().First(),
                _subscriptRow.GetMarks().First());
        }

        #endregion // Public_Methods
    }
}
