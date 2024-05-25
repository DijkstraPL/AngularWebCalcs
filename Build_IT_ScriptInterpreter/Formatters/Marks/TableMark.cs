using Build_IT_ScriptInterpreter.Formatters.Marks.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class TableMark : Mark
    {
        #region Properties

        public const string Code = "mtable";

        #endregion // Properties

        private MarkAttribute _columnSpacing;
        private MarkAttribute _columnAlign;

        #region Constructors

        public TableMark(params Mark[] childs) : base(Code)
        {
            if (childs != null)
                childs.ToList().ForEach(c => this.AddChild(c));
        }

        #endregion // Constructors


        #region Public_Methods

        public void SetColumnSpacing(double value, MathMLUnit unit)
        {
            if (_columnSpacing != null)
                this.RemoveAttribute(_columnSpacing);

            var unitValue = unit switch
            {
                MathMLUnit.EM => "em",
                MathMLUnit.EX => "ex",
                MathMLUnit.PX => "px",
                MathMLUnit.IN => "in",
                MathMLUnit.CM => "cm",
                MathMLUnit.MM => "mm",
                MathMLUnit.PT => "pt",
                MathMLUnit.PC => "pc",
                MathMLUnit.Percentage => "%",
                _ => throw new NotImplementedException()
            };

            _columnSpacing = new MarkAttribute("columnspacing", value.ToString(CultureInfo.InvariantCulture) + unitValue);
            this.AddAttribute(_columnSpacing);
        }
        public void SetColumnAlign(HorizontalAlign align)
        {
            if (_columnAlign != null)
                this.RemoveAttribute(_columnAlign);

            var alignValue = align switch
            {
                HorizontalAlign.Left => "left",
                HorizontalAlign.Center => "center",
                HorizontalAlign.Right => "right",
                _ => throw new NotImplementedException()
            };

            _columnAlign = new MarkAttribute("columnalign", alignValue);
            this.AddAttribute(_columnAlign);
        }

        #endregion // Public_Methods
    }
}
