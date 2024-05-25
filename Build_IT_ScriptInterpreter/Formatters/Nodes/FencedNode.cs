using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class FencedNode : IMathMLContainer
    {
        #region Properties

        public IList<IMathMLNode> NestedNodes { get; } = new List<IMathMLNode>();

        #endregion // Properties

        #region Fields

        private readonly IMathMLContainer _rowNode;

        #endregion // Fields

        #region Constructors

        public FencedNode()
        {
            _rowNode = new RowNode();
            _rowNode.AddRange(NestedNodes);
        }

        #endregion // Constructors

        #region Public_Methods

        public void Add(IMathMLNode node)
        {
            _rowNode.Add(node);
        }

        public void AddRange(IList<IMathMLNode> nodes)
        {
            foreach (var node in nodes)
                _rowNode.Add(node);
        }

        public IEnumerable<Mark> GetMarks()
        {
            yield return new FencedMark(_rowNode.GetMarks().ToArray());
        }

        #endregion // Public_Methods
    }
}
