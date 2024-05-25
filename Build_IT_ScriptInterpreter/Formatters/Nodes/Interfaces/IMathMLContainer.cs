using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces
{
    public interface IMathMLContainer : IMathMLNode
    {
        #region Properties
        
        IList<IMathMLNode> NestedNodes { get; }

        #endregion // Properties
        
        #region Public_Methods

        void Add(IMathMLNode node);
        void AddRange(IList<IMathMLNode> nodes);

        #endregion // Public_Methods
    }
}
