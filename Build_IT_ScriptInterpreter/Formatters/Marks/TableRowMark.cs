using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class TableRowMark : Mark
    {
        #region Properties

        public const string Code = "mtr";

        #endregion // Properties

        #region Constructors

        public TableRowMark(params Mark[] childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors
    }
}
