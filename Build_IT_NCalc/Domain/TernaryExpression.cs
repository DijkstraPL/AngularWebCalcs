namespace NCalc.Domain
{
	public class TernaryExpression : LogicalExpression
    {
        #region Properties
        
        public LogicalExpression LeftExpression { get; set; }
        public LogicalExpression MiddleExpression { get; set; }
        public LogicalExpression RightExpression { get; set; }

        #endregion // Properties

        #region Constructors
        
        public TernaryExpression(LogicalExpression leftExpression, LogicalExpression middleExpression, LogicalExpression rightExpression)
        {
            this.LeftExpression = leftExpression;
            this.MiddleExpression = middleExpression;
            this.RightExpression = rightExpression;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override void Accept(LogicalExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion // Public_Methods
    }   
}