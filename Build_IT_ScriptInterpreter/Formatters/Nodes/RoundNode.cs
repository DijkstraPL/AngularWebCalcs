using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class RoundNode : IMathMLNode
    {
        #region Fields

        private readonly string _value;

        #endregion // Fields

        #region Constructors

        public RoundNode(string value)
        {
            _value = value;
        }

        #endregion // Constructors

        #region Public_Methods

        public IEnumerable<Mark> GetMarks()
        {
            var split = MathMLHelper.Split(_value);
            var formatter = new MathMLFormatter();
            var innerNodes = formatter.GetInnerNodes(split.First());
            return innerNodes.SelectMany(nn => nn.GetMarks());
        }

        #endregion // Public_Methods
    }
}