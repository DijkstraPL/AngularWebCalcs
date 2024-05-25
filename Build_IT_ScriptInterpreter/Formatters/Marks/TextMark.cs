using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class TextMark : Mark
    {
        #region Properties

        public const string Code = "mtext";

        #endregion // Properties

        #region Constructors

        public TextMark(string value) : base(Code, value)
        {
        }

        #endregion // Constructors
    }
}
