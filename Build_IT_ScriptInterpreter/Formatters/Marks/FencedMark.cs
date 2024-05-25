using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class FencedMark : Mark
    {
        #region Properties

        public const string Code = "mfenced";

        #endregion // Properties

        #region Fields


        private MarkAttribute _open;
        private MarkAttribute _close;

        #endregion // Fields

        #region Constructors

        public FencedMark(params Mark[] childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors

        #region Public_Methods
        
        public void SetOpenSymbol(string symbol)
        {
            if (_open != null)
                this.RemoveAttribute(_open);
            _open = new MarkAttribute("open", symbol);
            this.AddAttribute(_open);
        }
        public void SetCloseSymbol(string symbol)
        {
            if (_close != null)
                this.RemoveAttribute(_close);
            _close = new MarkAttribute("close", symbol);
            this.AddAttribute(_close);
        }

        #endregion // Public_Methods
    }
}
