using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class SubscriptMark : Mark
    {
        #region Properties

        public const string Code = "msub";

        #endregion // Properties

        #region Constructors
        
        public SubscriptMark(Mark valueMark, Mark subscriptMark) : base(Code)
        {
            if (valueMark == null)
                throw new ArgumentNullException(nameof(valueMark));
            if (subscriptMark == null)
                throw new ArgumentNullException(nameof(subscriptMark));

            this.AddChild(valueMark);
            this.AddChild(subscriptMark);
        }

        #endregion // Constructors
    }
}
