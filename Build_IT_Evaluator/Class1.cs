using System;
using System.Linq.Expressions;

namespace Build_IT_Evaluator
{
    //public class AddExpression : BinaryExpression
    //{
    //    public override Expression Calculate()
    //    {

    //        if(LeftExpression is PropertyExpression propertyExpressionA && RightExpression is PropertyExpression propertyExpressionB)
    //        {
    //            return propertyExpressionA.Add(propertyExpressionB);
    //        }

    //        return LeftExpression.Calculate(); 
    //    }
    //}

    //public abstract class BinaryExpression
    //{
    //    public Expression LeftExpression { get; set; }
    //    public Expression RightExpression { get; set; }

    //    public abstract Expression Calculate();
    //}

    //public abstract class Expression
    //{
    //    public abstract Expression Add(Expression expression);
    //}

    //public class PropertyExpression : Expression
    //{
    //    public double Value { get; set; }

    //    public PropertyExpression()
    //    {
    //        ParameterExpression
    //    }

    //    public override PropertyExpression Add(PropertyExpression expression)
    //    {
    //        return new PropertyExpression { Value = Value + expression.Value };
    //    }
    //}

    //public abstract class ExpressionVisitor
    //{
    //    public abstract Expression VisitBinary(BinaryExpression binaryExpression);

    //}
}
