using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class RowMark : Mark
    {
        #region Properties

        public const string Code = "mrow";

        #endregion // Properties

        #region Constructors

        public RowMark(IEnumerable<Mark> childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors
    }
}
