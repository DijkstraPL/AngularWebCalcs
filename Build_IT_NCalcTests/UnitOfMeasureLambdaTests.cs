using Build_IT_NCalc;
using Build_IT_NCalc.Units;
using FluentAssertions;
using NCalc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Build_IT_NCalcTests
{
    public class UnitOfMeasureLambdaTests
    {
        [Fact]
        public void UnitOfMeasure_LambdaConversion()
        {
            var expr = new Expression("a - b - x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("x", expr.ValueUnit(1, "m"));
            expr.AddParameter("a", expr.ValueUnit(16, "m"));
            expr.AddParameter("b", expr.ValueUnit(6, "m"));

            var f = expr.ToLambda<ValueUnit>();
            var result = f();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(9, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void UnitOfMeasure_LambdaConversion_DifferentUnits()
        {
            var expr = new Expression("a - b - x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("x", expr.ValueUnit(60, "s"));
            expr.AddParameter("a", expr.ValueUnit(3, "min"));
            expr.AddParameter("b", expr.ValueUnit(1, "min"));

            var f = expr.ToLambda<ValueUnit>();
            var result = f();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.0 / 60, ((ValueUnit)result).Value);
            Assert.Contains("hr", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void UnitOfMeasure_LambdaConversion_DifferentUnits2()
        {
            var expr = new Expression("a - b - x", EvaluateOptions.AllowUnitCalculations);

            var f = expr.ToLambda<Context, ValueUnit>();
            var result = f(new Context
            {
                x = expr.ValueUnit(60, "s"),
                a = expr.ValueUnit(3, "min"),
                b = expr.ValueUnit(1, "min"),
            });

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.0 / 60, ((ValueUnit)result).Value);
            Assert.Contains("hr", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
    }

    internal class Context
    {
        public ValueUnit a { get; set; }
        public ValueUnit b { get; set; }
        public ValueUnit x { get; set; }
    }
}
