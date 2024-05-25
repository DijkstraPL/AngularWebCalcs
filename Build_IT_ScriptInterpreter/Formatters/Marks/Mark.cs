using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class Mark 
    {
        #region Properties

        public Mark Parent { get; private set; }
        public IList<Mark> ChildMarks { get; } = new List<Mark>();
        public string Value { get; }

        internal int Indent => Parent != null ? Parent.Indent + 2 : 0;

        #endregion // Properties

        #region Fields

        private readonly string _nodeCode;
        private readonly IList<MarkAttribute> _attributes = new List<MarkAttribute>();

        #endregion // Fields

        #region Constructors

        public Mark(string nodeCode, string value = "")
        {
            Parent?.ChildMarks.Add(this);
            Value = value;
            _nodeCode = nodeCode;
        }

        #endregion // Constructors

        #region Public_Methods

        public void SetParent(Mark parent)
        {
            Parent = parent;
            Parent?.ChildMarks.Add(this);
        }
        public void AddChild(Mark child)
        {            
            child.SetParent(this);
        }

        public void AddAttribute(MarkAttribute attribute)
        {
            _attributes.Add(attribute);
        }
        public void RemoveAttribute(MarkAttribute attribute)
        {
            _attributes.Remove(attribute);
        }

        public string GetNodeText()
        {
            if (_nodeCode == null)
                return Value;

            var stringBuilder = new StringBuilder();

            stringBuilder
                .Append(' ', Indent)
                .Append("<")
                .Append(_nodeCode);

            _attributes.ToList()
                .ForEach(a => stringBuilder
                        .Append(" ")
                        .Append(a.Name)
                        .Append("=\"")
                        .Append(a.Value)
                        .Append("\""));

            stringBuilder.Append(">");

            if (!string.IsNullOrWhiteSpace(Value))
                stringBuilder.Append(Value);
            else
            {
                ChildMarks.ToList()
                    .ForEach(cm => stringBuilder
                        .Append('\n')
                        .Append(cm.ToString()));
                stringBuilder.Append('\n')
                        .Append(' ', Indent);
            }

            stringBuilder
                .Append("</")
                .Append(_nodeCode)
                .Append(">");

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return GetNodeText();
        }

        #endregion // Public_Methods
    }
}
