using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class OperationMark : Mark
    {
        #region Properties

        public const string Code = "mo";

        #endregion // Properties

        #region Constructors
        
        public OperationMark(string value) : base(Code, value)
        {
        }

        #endregion // Constructors
    }
}
