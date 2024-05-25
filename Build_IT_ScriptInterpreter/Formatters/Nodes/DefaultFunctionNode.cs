using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class DefaultFunctionNode : IMathMLNode
    {
        #region Fields
        
        private readonly string _funtionName;
        private readonly string _value;
        private readonly IList<IMathMLNode> _argsRows = new List<IMathMLNode>();

        #endregion // Fields

        #region Constructors

        public DefaultFunctionNode(string functionName, string value)
        {
            _funtionName = functionName;
            _value = value;

            var args = MathMLHelper.Split(_value);

            foreach (var arg in args)
            {
                var formatter = new MathMLFormatter();
                var argNodes = formatter.Format(arg);
                var argRow = new RowNode();
                argRow.AddRange(argNodes);
                _argsRows.Add(argRow);
            }
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            var functionName = new IdentifierMark(_funtionName.ToLower());
            var fencedMark = new FencedMark(_argsRows.SelectMany(r => r.GetMarks()).ToArray());

            yield return functionName;
            yield return fencedMark;
        }

        #endregion // Public_Methods
    }
}
