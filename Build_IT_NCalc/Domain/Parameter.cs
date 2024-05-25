namespace NCalc.Domain
{
	public class Identifier : LogicalExpression
    {
        #region Properties
        
        public string Name { get; set; }

        #endregion // Properties

        #region Constructors

        public Identifier(string name)
        {
            Name = name;
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
