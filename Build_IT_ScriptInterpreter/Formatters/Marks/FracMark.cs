using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class FracMark : Mark
    {
        #region Properties

        public const string Code = "mfrac";

        #endregion // Properties

        #region Constructors

        public FracMark(IEnumerable<Mark> childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors
    }
}
