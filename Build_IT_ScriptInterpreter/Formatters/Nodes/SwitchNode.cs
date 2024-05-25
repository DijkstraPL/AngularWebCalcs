using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class SwitchNode : IMathMLNode
    {
        #region Fields

        private readonly string _value;
        private readonly IMathMLNode _switchValue;
        private readonly IList<(IMathMLNode expected, IMathMLNode value)> _cases = new List<(IMathMLNode, IMathMLNode)>();

        #endregion // Fields

        #region Contructors

        public SwitchNode(string value)
        {
            _value = value;

            var formatter = new MathMLFormatter();

            var split = MathMLHelper.Split(_value).ToArray();

            _switchValue = formatter.GetMathML(split.First());

            for (int i = 1; i < split.Length; i += 2)
                _cases.Add((formatter.GetMathML(split[i]), formatter.GetMathML(split[i + 1])));
        }

        #endregion // Contructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            var rows = new List<Mark>();

            var @switch = new IdentifierMark("select");
            var tableConditionStatement = new TableCellMark(@switch, _switchValue.GetMarks().First());
            rows.Add(new TableRowMark(tableConditionStatement));

            foreach (var @case in _cases)
            {
                var excpectedCell = new TableCellMark(@case.expected.GetMarks().First());
                var resultCell = new TableCellMark(@case.value.GetMarks().First());
                rows.Add(new TableRowMark(excpectedCell, resultCell));
            }
            
            var table = new TableMark(rows.ToArray());
            table.SetColumnSpacing(1.4, Marks.Enums.MathMLUnit.EX);
            table.SetColumnAlign(Marks.Enums.HorizontalAlign.Left);

            var fence = new FencedMark(table);
            fence.SetOpenSymbol("{");
            fence.SetCloseSymbol("");
            yield return fence;
        }

        #endregion // Public_Methods
    }
}
