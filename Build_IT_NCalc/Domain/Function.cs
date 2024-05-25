namespace NCalc.Domain
{
	public class Function : LogicalExpression
    {
        #region Properties
        
        public Identifier Identifier { get; set; }
        public LogicalExpression[] Expressions { get; set; }

        #endregion // Properties

        #region Constructors

        public Function(Identifier identifier, LogicalExpression[] expressions)
        {
            Identifier = identifier;
            Expressions = expressions;
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
