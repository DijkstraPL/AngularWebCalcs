using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class PowerNode : IMathMLNode
    {
        #region Fields

        private readonly IMathMLContainer _valueRow;
        private readonly IMathMLContainer _powerRow;

        #endregion // Fields

        #region Constructors

        public PowerNode(string value)
        {
            _valueRow = new RowNode();
            _powerRow = new RowNode();

            var valueFormatter = new MathMLFormatter();
            var valueNodes = valueFormatter.Format(MathMLHelper.Split(value).First());
            if (valueNodes.Count > 1)
            {
                var fence = new FencedNode();
                fence.AddRange(valueNodes);
                _valueRow.Add(fence);
            }
            else
                _valueRow.AddRange(valueNodes);

            var powerFormatter = new MathMLFormatter();
            var powerNodes = powerFormatter.Format(MathMLHelper.Split(value).Last());
            _powerRow.AddRange(powerNodes);
        }

        #endregion // Constructors

        #region Public_Methods
        
        public IEnumerable<Mark> GetMarks()
        {
            yield return new SupscriptMark(
                _valueRow.GetMarks().First(), 
                _powerRow.GetMarks().First());
        }

        #endregion // Public_Methods
    }
}
