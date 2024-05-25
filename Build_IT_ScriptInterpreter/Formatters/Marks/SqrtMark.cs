using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class SqrtMark : Mark
    {
        #region Properties

        public const string Code = "msqrt";

        #endregion // Properties

        #region Constructors
        
        public SqrtMark(IEnumerable<Mark> childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors
    }
}
