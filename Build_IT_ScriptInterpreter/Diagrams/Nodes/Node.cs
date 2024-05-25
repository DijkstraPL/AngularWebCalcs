using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Diagrams.Nodes.Builders;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Interfaces;
using NCalc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreter.Diagrams.Nodes
{

    public abstract class Node
    {
        protected Node _nextNode;

        public static EntryPointNodeBuilder Builder(Action<NodeBuilder> setup)
        {
            var entryPointBuilder = new EntryPointNodeBuilder();
            setup(entryPointBuilder);
            return entryPointBuilder;
        }

        public CalculatedNodes CalculateNode()
        {
            try
            {
                var calculatedNodes = CalculateNode(new List<CalculatedNode>());
                return new CalculatedNodes(calculatedNodes);
            }
            catch
            {
                throw;
            }
        }
        public CalculatedNodes CalculateNodeNoLambda()
        {
            try
            {
                var calculatedNodes = CalculateNodeNoLambda(new List<CalculatedNode>());
                return new CalculatedNodes(calculatedNodes);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something goes wrong during node calculation", ex);
            }
        }

        public abstract bool IsValid();
        public abstract void Initialize(IEnumerable<ParameterMetadata> parameters);

        protected abstract CalculatedNode Calculate(IEnumerable<CalculatedNode> calculatedNodes);
        protected abstract CalculatedNode CalculateNoLambda(IEnumerable<CalculatedNode> calculatedNodes);

        private IEnumerable<CalculatedNode> CalculateNode(List<CalculatedNode> calculatedNodes)
        {
            if (!IsValid())
                throw new ArgumentException("Node is not valid");

            var result = Calculate(calculatedNodes);
            calculatedNodes.Add(result);
            _nextNode?.CalculateNode(calculatedNodes);
            return calculatedNodes;
        }
        private IEnumerable<CalculatedNode> CalculateNodeNoLambda(List<CalculatedNode> calculatedNodes)
        {
            if (!IsValid())
                throw new ArgumentException("Node is not valid");

            var result = CalculateNoLambda(calculatedNodes);
            calculatedNodes.Add(result);
            _nextNode?.CalculateNodeNoLambda(calculatedNodes);
            return calculatedNodes;
        }

        public virtual void SetValue(string parameterName, object value)
        {
            _nextNode?.SetValue(parameterName, value);
        }

        public void SetValue(string parameterName, double value, params string[] units)
        {
            SetValue(parameterName, new ValueUnit(value, units));
        }
    }
}
