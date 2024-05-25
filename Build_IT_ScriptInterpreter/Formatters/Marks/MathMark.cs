using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class MathMark : Mark
    {
        #region Properties

        public const string Code = "math";

        #endregion // Properties

        #region Fields
        
        private MarkAttribute _mathAttribute = new MarkAttribute("xmlns", "http://www.w3.org/1998/Math/MathML");

        #endregion // Fields

        #region Constructors
        
        public MathMark(IEnumerable<Mark> childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
            this.AddAttribute(_mathAttribute);
        }

        #endregion // Constructors
    }
}
