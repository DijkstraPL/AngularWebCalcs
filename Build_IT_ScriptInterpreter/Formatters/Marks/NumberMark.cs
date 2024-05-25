using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class NumberMark : Mark
    {
        #region Properties

        public const string Code = "mn";

        #endregion // Properties

        #region Constructors
        
        public NumberMark(string value) : base(Code, value)
        {
        }

        #endregion // Constructors
    }
}
