namespace Build_IT_ScriptInterpreter.Expressions.Parameters.Interfaces
{
    public interface ICustomParameter<T> : ICustomParameter
    {
        #region Properties

        new T Value { get; }

        #endregion // Properties
    }

    public interface ICustomParameter
    {
        #region Properties

        string[] Names { get; }
        object Value { get; }

        #endregion // Properties
    }
}
