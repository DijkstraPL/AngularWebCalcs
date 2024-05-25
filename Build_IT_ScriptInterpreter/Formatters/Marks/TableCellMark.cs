using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class TableCellMark : Mark
    {
        #region Properties

        public const string Code = "mtd";

        #endregion // Properties

        #region Constructors

        public TableCellMark(params Mark[] childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors
    }
}
