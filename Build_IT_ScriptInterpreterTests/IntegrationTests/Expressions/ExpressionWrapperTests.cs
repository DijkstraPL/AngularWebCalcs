using Build_IT_ScriptInterpreter.Expressions;
using NCalc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Expressions
{
        [TestFixture]
    public class ExpressionTests
    {
        [Test]
        public void EvaluateTest_Success()
        {
            var expression = new Expression("2+2*2");

            var result = expression.Evaluate().ToString();

            Assert.That(result, Is.EqualTo("6"));
        }

        [Test]
        public void EvaluateWithParametersTest_Success()
        {
            var expression = new Expression("[a]+[b]*[c]");

            expression.SetParameters(new Dictionary<string, object>
            {
                {"a", 1 },
                {"b", 2 },
                {"c", 3 }
            });
            var result = expression.Evaluate().ToString();

            Assert.That(result, Is.EqualTo("7"));
        }

        [Test]
        public void EvaluateWithAdditionalFunctionTest_Success()
        {
            var expression = new Expression("Test(1,2,3)+1");

            expression.SetAdditionalFunction("Test", fa => 
                (int)fa.Parameters[0].Evaluate() + (int)fa.Parameters[1].Evaluate() * (int)fa.Parameters[2].Evaluate()
            );
            var result = expression.Evaluate().ToString();

            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void ContainsParameterTest_ReturnsTrue()
        {
            var expression = new Expression("[a]+[b]");

            var result = expression.ContainsParameter("a");

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsParameterTest_ReturnsFalse()
        {
            var expression = new Expression("[a]+[b]");

            var result = expression.ContainsParameter("c");

            Assert.IsFalse(result);
        }
        
        [Test]
        public void EvaluateWithListParametersTest_ShouldReturnSingleValue()
        {
            var expression = new Expression("if([a]=='aa',[b],[c])", 
                NCalc.EvaluateOptions.IgnoreCase);

            var par = new Dictionary<string, object>
            {
                {"a", "aa" },
                {"b", 2 },
                {"c", new int[]{ 3,4 } }
            };
            expression.SetParameters(par);
            var result = expression.Evaluate();

            Assert.That(result.ToString(), Is.EqualTo("2"));
        }

        [Test]
        public void EvaluateWithListParametersTest_ShouldReturnMultipleValues()
        {
            var expression = new Expression("if([a]=='bb',[b],[c])",
                NCalc.EvaluateOptions.IgnoreCase);

            var par = new Dictionary<string, object>
            {
                {"a", "aa" },
                {"b", 2 },
                {"c", new int[]{ 3,4 } }
            };
            expression.SetParameters(par);
            var result = expression.Evaluate() as int[];

            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(3));
            Assert.That(result[1], Is.EqualTo(4));
        }
    }
}
