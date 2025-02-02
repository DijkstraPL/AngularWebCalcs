namespace NCalc.Domain
{
	public class BinaryExpression : LogicalExpression
    {
        #region Properties
        
        public LogicalExpression LeftExpression { get; set; }
        public LogicalExpression RightExpression { get; set; }
        public BinaryExpressionType Type { get; set; }

        #endregion // Properties

        #region Constructors
        
        public BinaryExpression(BinaryExpressionType type, LogicalExpression leftExpression, LogicalExpression rightExpression)
        {
            Type = type;
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override void Accept(LogicalExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion // Public_Methods
    }

    public enum BinaryExpressionType
	{
		And,
		Or,
		NotEqual,
		LesserOrEqual,
		GreaterOrEqual,
		Lesser,
		Greater,
		Equal,
		Minus,
		Plus,
		Modulo,
		Div,
        Times,
        BitwiseOr,
        BitwiseAnd,
        BitwiseXOr,
        LeftShift,
        RightShift,
        Unknown
	}
}
