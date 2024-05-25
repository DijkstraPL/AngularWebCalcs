using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class IfNode : IMathMLNode
    {
        #region Fields

        private readonly string _value;
        private readonly IMathMLContainer _conditionRow;
        private readonly IMathMLContainer _trueRow;
        private readonly IMathMLContainer _falseRow;

        #endregion // Fields

        #region Contructors

        public IfNode(string value)
        {
            _value = value;

            var split = MathMLHelper.Split(_value).ToArray();

            var condition = split[0];
            var ifTrueValue = split[1];
            var ifFalseValue = split[2];

            var conditionFormatter = new MathMLFormatter();
            var conditionNodes = conditionFormatter.Format(condition);
            _conditionRow = new RowNode();
            _conditionRow.AddRange(conditionNodes);

            var trueValueFormatter = new MathMLFormatter();
            var trueNodes = trueValueFormatter.Format(ifTrueValue);
            _trueRow = new RowNode();
            _trueRow.AddRange(trueNodes);

            var falseValueFormatter = new MathMLFormatter();
            var falseNodes = falseValueFormatter.Format(ifFalseValue);
            _falseRow = new RowNode();
            _falseRow.AddRange(falseNodes);
        }

        #endregion // Contructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            var rows = new List<TableRowMark>();
            rows.Add(GetTrueRow("if&#xA0;"));

            if (_falseRow.NestedNodes.Count == 1 && _falseRow.NestedNodes.First() is IfNode nestedIfNode)
            {
                rows.Add(nestedIfNode.GetTrueRow("else&#xA0;if&#xA0;"));
                rows.AddRange(nestedIfNode.GetFalseRows());
            }
            else
                rows.AddRange(GetFalseRows());

            var table = new TableMark(rows.ToArray());
            table.SetColumnSpacing(1.4, Marks.Enums.MathMLUnit.EX);
            table.SetColumnAlign(Marks.Enums.HorizontalAlign.Left);

            var fence = new FencedMark(table);
            fence.SetOpenSymbol("{");
            fence.SetCloseSymbol("");
            yield return fence;
        }

        public TableRowMark GetTrueRow(string identifierMarkText)
        {
            var @if = new IdentifierMark(identifierMarkText);
            var tableConditionStatement = new TableCellMark(@if, _conditionRow.GetMarks().First());
            var tableIfTrueData = new TableCellMark(_trueRow.GetMarks().First());
            var tableFirstRow = new TableRowMark(tableConditionStatement, tableIfTrueData);
            return tableFirstRow;
        }

        public IEnumerable<TableRowMark> GetFalseRows()
        {
            if (_falseRow.NestedNodes.Count == 1 && _falseRow.NestedNodes.First() is IfNode nestedIfNode)
            {
                yield return nestedIfNode.GetTrueRow("else&#xA0;if&#xA0;");
                foreach (var falseRow in nestedIfNode.GetFalseRows())
                    yield return falseRow;
            }
            else
            {
                var @else = new IdentifierMark("else&#xA0;");
                var tableElseStatement = new TableCellMark(@else);
                var tableIfFalseData = new TableCellMark(_falseRow.GetMarks().First());
                yield return new TableRowMark(tableElseStatement, tableIfFalseData);
            }
        }

        #endregion // Public_Methods
    }
}
