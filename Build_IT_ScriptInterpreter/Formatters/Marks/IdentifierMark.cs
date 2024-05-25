using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class IdentifierMark : Mark
    {
        #region Properties

        public const string Code = "mi";

        #endregion // Properties

        #region Constructors
        
        public IdentifierMark(string value) : base(Code, value)
        {
        }

        #endregion // Constructors
    }
}
