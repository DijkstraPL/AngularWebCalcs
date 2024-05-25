namespace Build_IT_ScriptInterpreter.CalculationEngine.Visitors
{
    public abstract class ParameterStateVisitor<T>
    {
        public abstract T Visit(CalculatedParameterState calculatedParameterState);
        public abstract T Visit(CanBeCalculatedParameterState canBeCalculatedParameterState);
        public abstract T Visit(CalculationInputParameter calculationInputParameter);
        public abstract T Visit(EmptyParameter emptyParameter);
        public abstract T Visit(InputDataParameter inputDataParameter);
        public abstract T Visit(MissingDataParameterState missingDataParameterState);
        public abstract T Visit(UnknownErrorParameterState unknownErrorParameterState);
    }

    public abstract class ParameterStateVisitor 
    {
        public abstract void Visit(CalculatedParameterState calculatedParameterState);
        public abstract void Visit(CanBeCalculatedParameterState canBeCalculatedParameterState);
        public abstract void Visit(CalculationInputParameter calculationInputParameter);
        public abstract void Visit(EmptyParameter emptyParameter);
        public abstract void Visit(InputDataParameter inputDataParameter);
        public abstract void Visit(MissingDataParameterState missingDataParameterState);
        public abstract void Visit(UnknownErrorParameterState unknownErrorParameterState);
    }
}
