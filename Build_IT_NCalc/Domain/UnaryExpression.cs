namespace NCalc.Domain
{
	public class UnaryExpression : LogicalExpression
    {
        #region Properties
        
        public LogicalExpression Expression { get; set; }
        public UnaryExpressionType Type { get; set; }

        #endregion // Properties

        #region Constructors

        public UnaryExpression(UnaryExpressionType type, LogicalExpression expression)
        {
            Type = type;
            Expression = expression;
        }

        #endregion // Constructors

        #region Public_Methods

        public override void Accept(LogicalExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion // Public_Methods
    }

	public enum UnaryExpressionType
	{
		Not,
        Negate,
        BitwiseNot
	}
}
