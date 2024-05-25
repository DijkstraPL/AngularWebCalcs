namespace Build_IT_ScriptInterpreter.Formatters.Marks
{
    public class MarkAttribute
    {
        #region Properties
        
        public string Name { get; }
        public string Value { get; }
        
        #endregion // Properties

        #region Constructors

        public MarkAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        #endregion // Constructors
    }
}
