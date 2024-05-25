using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class SupscriptMark : Mark
    {
        #region Properties

        public const string Code = "msup";

        #endregion // Properties

        #region Constructors
        
        public SupscriptMark(Mark valueMark, Mark powerMark) : base(Code)
        {
            if (valueMark == null)
                throw new ArgumentNullException(nameof(valueMark));
            if (powerMark == null)
                throw new ArgumentNullException(nameof(powerMark));

            this.AddChild(valueMark);
            this.AddChild(powerMark);
        }

        #endregion // Constructors
    }
}
