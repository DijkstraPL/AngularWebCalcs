using Build_IT_NCalc.Units;
using Build_IT_NCalc.Units.LengthUnits;
using FluentAssertions.Execution;
using NCalc;
using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests
{
    public class UnitOfMeasureTests
    {
        [Fact]
        public void SubstractionOfParametersWithSameUnitsTest()
        {
            var expr = new Expression("Unit(2,'m') + Unit(2,'m') - a - b - x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("x", expr.ValueUnit(3, "m"));
            expr.AddParameter("a", expr.ValueUnit(2, "m"));
            expr.AddParameter("b", expr.ValueUnit(1, "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(-2, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void SubstractionOfParametersWithSameUnitsTest_ConvertToDouble()
        {
            var expr = new Expression("a - b - x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(10, "m"));
            expr.AddParameter("b", expr.ValueUnit(1, "m"));
            expr.AddParameter("x", expr.ValueUnit(3, "m"));

            var result = Convert.ToDouble(expr.Evaluate());
            Assert.IsType<double>(result);
            Assert.Equal(6, ((double)result));
        }

        [Fact]
        public void SubstractionOfParametersWithSameUnitsTest_ConvertToDouble_NotMainUnit()
        {
            var expr = new Expression("a - b - x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(10, "mm"));
            expr.AddParameter("b", expr.ValueUnit(1, "mm"));
            expr.AddParameter("x", expr.ValueUnit(3, "mm"));

            var result = Convert.ToDouble(expr.Evaluate());
            Assert.IsType<double>(result);
            Assert.Equal(6, ((double)result));
        }



        [Fact]
        public void AdditionOfParametersWithSameUnitsTest()
        {
            var expr = new Expression("Unit(2,'m') + Unit(2,'m') + a + b + x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("x", expr.ValueUnit(3, "m"));
            expr.AddParameter("a", expr.ValueUnit(2, "m"));
            expr.AddParameter("b", expr.ValueUnit(1, "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(10, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AdditionOfParametersWithDifferentUnitsTest_ThrowsArgumentException()
        {
            var expr = new Expression("2 + 2 + a + b + x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("x", expr.ValueUnit(3, "m"));
            expr.AddParameter("a", expr.ValueUnit(2, "kN"));
            expr.AddParameter("b", expr.ValueUnit(1, "m"));

            Assert.Throws<ArgumentException>(() => expr.Evaluate());
        }

        [Fact]
        public void AdditionOfParametersWithDifferentUnitsInSameCategoryTest()
        {
            var expr = new Expression("Unit(2,'m') + Unit(2,'m') + a + b + x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(1, "m"));
            expr.AddParameter("b", expr.ValueUnit(200, "mm"));
            expr.AddParameter("x", expr.ValueUnit(3, "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(8.2, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void SubstractionOfParametersWithDifferentUnitsInSameCategoryTest()
        {
            var expr = new Expression("a - b - x", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "m"));
            expr.AddParameter("b", expr.ValueUnit(200, "mm"));
            expr.AddParameter("x", expr.ValueUnit(1, "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.8, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Theory]
        [InlineData("a>b", 200, true)]
        [InlineData("a<b", 200, false)]
        [InlineData("a==b", 1000, true)]
        [InlineData("a==b", 500, false)]
        [InlineData("a!=b", 1000, false)]
        [InlineData("a!=b", 500, true)]
        [InlineData("a>=b", 1000, true)]
        [InlineData("a>=b", 1001, false)]
        [InlineData("a>=b", 999, true)]
        [InlineData("a<=b", 1000, true)]
        [InlineData("a<=b", 1001, true)]
        [InlineData("a<=b", 999, false)]
        public void ComparisonTest(string formula, double value, bool expectedResult)
        {
            var expr = new Expression(formula, EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(1, "m"));
            expr.AddParameter("b", expr.ValueUnit(value, "mm"));

            var result = expr.Evaluate();
            Assert.IsType<bool>(result);
            Assert.Equal(expectedResult, (bool)result);
        }

        [Fact]
        public void AdditionOfParametersWithDifferentUnitsInSameCategoryTest_TwoDividents()
        {
            var expr = new Expression("a + b + c", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "m", "kN"));
            expr.AddParameter("b", expr.ValueUnit(1, "kN", "m"));
            expr.AddParameter("c", expr.ValueUnit(3, new string[] { "m", "kN" }));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(6, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("6m*kN", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void AdditionOfParametersWithDifferentUnitsInDifferentCategoriesTest_ThrowsArgumentException()
        {
            var expr = new Expression("a + b + c", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "m", "kN"));
            expr.AddParameter("b", expr.ValueUnit(1, "kN"));
            expr.AddParameter("c", expr.ValueUnit(3, new string[] { "m", "kN" }));

            Assert.Throws<ArgumentException>(() => expr.Evaluate());
        }

        [Fact]
        public void ConvertDividendToUnitTest()
        {
            var unit = new ValueUnit(2, "m");
            unit.Transform<Meter, Milimeter>();

            Assert.IsType<ValueUnit>(unit);
            Assert.Equal(2000, ((ValueUnit)unit).Value);
            Assert.Contains("mm", ((ValueUnit)unit).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void ConvertDivisorToUnitTest()
        {
            var unit = new ValueUnit(2,  "kN", "mm^-1");
            unit.Transform<Milimeter, Meter>();

            Assert.IsType<ValueUnit>(unit);
            Assert.Equal(2000, ((ValueUnit)unit).Value);
            Assert.Contains("kN", ((ValueUnit)unit).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)unit).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void MultiplyParametersWithDifferentUnitsInSameCategoryTest()
        {
            var expr = new Expression("a*b", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "m"));
            expr.AddParameter("b", expr.ValueUnit(200, "mm"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(400, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("mm", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("400m*mm", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void MultiplyParametersWithDifferentUnitsInDifferentCategoriesTest()
        {
            var expr = new Expression("a*b", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "kN"));
            expr.AddParameter("b", expr.ValueUnit(200, "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(400, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("400kN*m", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void MultiplyParametersWithDifferentUnitsInDifferentCategoriesTest_MoreThan2()
        {
            var expr = new Expression("a*b*c", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "kN"));
            expr.AddParameter("b", expr.ValueUnit(200, "m"));
            expr.AddParameter("c", expr.ValueUnit(10, "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(4000, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("4000kN*m^2", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void MultiplyParametersWithDifferentUnitsInDifferentCategoriesTest_MoreThan2_DifferentUnits()
        {
            var expr = new Expression("a*b*c", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(2, "kN"));
            expr.AddParameter("b", expr.ValueUnit(200, "m"));
            expr.AddParameter("c", expr.ValueUnit(10, "cm"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(4000, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("cm", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("4000kN*m*cm", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void DivideParametersWithDifferentUnitsInDifferentCategoriesTest()
        {
            var expr = new Expression("a/b", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(10,  "kN"));
            expr.AddParameter("b", new ValueUnit(5,  "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("2kN/m", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void DivideParametersWithDifferentUnitsInSameCategoryTest()
        {
            var expr = new Expression("a/b", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1000,  "mm"));
            expr.AddParameter("b", new ValueUnit(5,  "m"));

            var result = expr.Evaluate();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(200, ((ValueUnit)result).Value);
            Assert.Contains("mm", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("200mm/m", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void MoreComplicatedTest_NoUnitForSomeValues()
        {
            var expr = new Expression("a*b/c", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(10, "cm", "cm"));
            expr.AddParameter("b", new ValueUnit(235, "MPa"));
            expr.AddParameter("c", new ValueUnit(1));

            var result = expr.Evaluate();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2350, ((ValueUnit)result).Value);
            Assert.Contains("MPa", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("cm", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("2350cm^2*MPa", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void UnknownUnits_ShouldAddNewUnit()
        {
            using (new AssertionScope())
            {
                var expr = new Expression("a * b", EvaluateOptions.AllowUnitCalculations);

                expr.AddParameter("a", new ValueUnit(2,  "aa"));
                expr.AddParameter("b", new ValueUnit(3,  "m"));

                var result = expr.Evaluate();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(6, ((ValueUnit)result).Value);
                Assert.Contains("aa", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            }
        }

        [Fact]
        public void DivideParametersWithDifferentUnitsInDifferentCategoriesTest_OrganizeUnitsShouldRemoveNotNeededOnes()
        {
            using (new AssertionScope())
            {
                var expr = new Expression("a/b+c", EvaluateOptions.AllowUnitCalculations);

                expr.AddParameter("a", expr.ValueUnit(10, "kN"));
                expr.AddParameter("b", expr.ValueUnit(5, "m", "m"));
                expr.AddParameter("c", expr.ValueUnit(2, "kPa"));

                var result = expr.Evaluate() as ValueUnit;
                result.OrganizeUnits();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(4, ((ValueUnit)result).Value);
                Assert.Contains("kPa", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("4kPa", ((ValueUnit)result).ToString());
            }
        }

        [Fact]
        public void DivideParametersWithDifferentUnitsInDifferentCategoriesTest_OrganizeUnitsShouldRemoveNotNeededOnes_Division()
        {
            using (new AssertionScope())
            {
                var expr = new Expression("(a*b)/c", EvaluateOptions.AllowUnitCalculations);

                expr.AddParameter("a", expr.ValueUnit(10, "kg"));
                expr.AddParameter("b", expr.ValueUnit(5, "m"));
                expr.AddParameter("c", expr.ValueUnit(2, "s", "s"));

                var result = expr.Evaluate() as ValueUnit;
                result.OrganizeUnits();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(0.025, ((ValueUnit)result).Value);
                Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("0.025kN", ((ValueUnit)result).ToString(CultureInfo.InvariantCulture));
            }
        }

        [Fact]
        public void EvaluateParametersEvent()
        {
            using (new AssertionScope())
            {
                var unitExpression = new Expression("Max([Param1] * 7, [Param2])", EvaluateOptions.AllowUnitCalculations);
                unitExpression.EvaluateParameter += (name, args) =>
                {
                    if (name == "Param1") args.Result = unitExpression.ValueUnit(50, "mm");
                    if (name == "Param2") args.Result = unitExpression.ValueUnit(1, "m");
                };

                var result = unitExpression.Evaluate();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(1, ((ValueUnit)result).Value);
                Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("1m", ((ValueUnit)result).ToString());
            }
        }

        [Fact]
        public void AddingDoubleToValueUnit_WrongParameters()
        {
            using (new AssertionScope())
            {
                var unitExpression = new Expression("[Param1]+[Param2]", EvaluateOptions.AllowUnitCalculations);
                unitExpression.EvaluateParameter += (name, args) =>
                {
                    if (name == "Param1") args.Result = unitExpression.ValueUnit(5, "m");
                    if (name == "Param2") args.Result = 2;
                };

                Assert.Throws<ArgumentException>(() => unitExpression.Evaluate());
            }
        }
        [Fact]
        public void AddingDoubleToValueUnit_ProperParameters()
        {
            using (new AssertionScope())
            {
                var unitExpression = new Expression("[Param1]+[Param2]", EvaluateOptions.AllowUnitCalculations);
                unitExpression.EvaluateParameter += (name, args) =>
                {
                    if (name == "Param1") args.Result = unitExpression.ValueUnit(5, "m");
                    if (name == "Param2") args.Result = unitExpression.ValueUnit(2,"m");
                };

                var result = unitExpression.Evaluate();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(7, ((ValueUnit)result).Value);
                Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("7m", ((ValueUnit)result).ToString());
            }
        }
    }
}
