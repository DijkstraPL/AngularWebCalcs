using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Functions;
using NCalc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Functions
{
    [TestFixture]
    public class ErrorFunctionTests
    {
        [Test]
        public void ErrorFunctionTest_FunctionWithOneParameter_ShouldThrowCalculationException()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(1,  "m"),
                    ["b"] = new ValueUnit(11,  "cm"),
                    ["c"] = "Value is too big"
                };

                var function = ExpressionEvaluator.Create("if([a]>[b],ERROR([c]),1)", parameters);

                Assert.Throws<CalculationException>(() => function.Evaluate());
        }

        [Test]
        public void ErrorFunctionTest_FunctionWithManyParameters_ShouldThrowArgumentException()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(1,  "m"),
                    ["b"] = new ValueUnit(11,  "cm"),
                    ["c"] = "Value is too big"
                };

                var function = ExpressionEvaluator.Create("if([a]>[b],ERROR([c],2),1)", parameters);

                Assert.Throws<ArgumentException>(() => function.Evaluate());
        }

        [Test]
        public void ErrorFunctionTest_FunctionWithOneParameter_ShouldSkipTheError()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(1, "m"),
                    ["b"] = new ValueUnit(11, "cm"),
                    ["c"] = "Value is too big"
                };

                var function = ExpressionEvaluator.Create("if([a]<[b],ERROR([c]),1)", parameters);
                var result = function.Evaluate();

                Assert.That(((int)result), Is.EqualTo(1));
        }
    }
}
