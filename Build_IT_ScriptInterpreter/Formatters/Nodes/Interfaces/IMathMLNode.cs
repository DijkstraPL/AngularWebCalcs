using Build_IT_ScriptInterpreter.Formatters.Marks;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces
{
    public interface IMathMLNode
    {
        #region Public_Methods

        IEnumerable<Mark> GetMarks();

        #endregion // Public_Methods
    }
}
