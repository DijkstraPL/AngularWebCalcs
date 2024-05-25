
using System.Collections.Generic;

namespace NCalc.Domain
{
    public class ParameterCounterExpressionVisitor : LogicalExpressionVisitor
    {
        public List<string> Parameters { get; } = new List<string>();

        public override void Visit(LogicalExpression expression)
        {
        }

        public override void Visit(TernaryExpression expression)
        {
            expression.LeftExpression.Accept(this);
            expression.MiddleExpression.Accept(this);
            expression.RightExpression.Accept(this);
        }

        public override void Visit(BinaryExpression expression)
        {
            expression.LeftExpression.Accept(this);
            expression.RightExpression.Accept(this);
        }

        public override void Visit(UnaryExpression expression)
        {
            expression.Expression.Accept(this);
        }

        public override void Visit(ValueExpression expression)
        {
        }

        public override void Visit(Function function)
        {
            foreach (var expression in function.Expressions)
                expression.Accept(this);
        }

        public override void Visit(Identifier function)
        {
            if (!Parameters.Contains(function.Name))
                Parameters.Add(function.Name);
        }
    }
}
