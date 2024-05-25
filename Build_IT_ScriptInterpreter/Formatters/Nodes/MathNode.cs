using Build_IT_ScriptInterpreter.Formatters.Marks;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes
{
    internal class MathNode : IMathMLContainer
    {
        #region Properties
        
        public IList<IMathMLNode> NestedNodes { get; } = new List<IMathMLNode>();

        #endregion // Properties

        #region Public_Methods
        
        public void Add(IMathMLNode node)
        {
            NestedNodes.Add(node);
        }

        public void AddRange(IList<IMathMLNode> nodes)
        {
            foreach (var node in nodes)
                Add(node);
        }

        public IEnumerable<Mark> GetMarks()
        {
            yield return new MathMark(NestedNodes.SelectMany(nn => nn.GetMarks()));
        }

        #endregion // Public_Methods
    }
}
