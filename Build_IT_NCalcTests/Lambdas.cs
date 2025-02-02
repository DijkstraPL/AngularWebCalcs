﻿using System;
using System.Collections.Generic;
using Build_IT_NCalc.Units;
using FluentAssertions;
using Xunit;
using LQ = System.Linq.Expressions;

namespace NCalc.Tests
{
    public class Lambdas
    {
        private class Context
        {
            public int FieldA { get; set; }
            public string FieldB { get; set; }
            public decimal FieldC { get; set; }
            public decimal? FieldD { get; set; }
            public int? FieldE { get; set; }

            public int Test(int a, int b)
            {
                return a + b;
            }

            public string Test(string a, string b)
            {
                return a + b;
            }

            public int Test(int a, int b, int c)
            {
                return a + b + c;
            }

            public string Sum(string msg, params int[] numbers)
            {
                int total = 0;
                foreach (var num in numbers)
                {
                    total += num;
                }

                return msg + total;
            }

            public int Sum(params int[] numbers)
            {
                int total = 0;
                foreach (var num in numbers)
                {
                    total += num;
                }

                return total;
            }

            public int Sum(TestObject1 obj1, TestObject2 obj2)
            {
                return obj1.Count1 + obj2.Count2;
            }

            public int Sum(TestObject2 obj1, TestObject1 obj2)
            {
                return obj1.Count2 + obj2.Count1;
            }

            public int Sum(TestObject1 obj1, TestObject1 obj2)
            {
                return obj1.Count1 + obj2.Count1;
            }

            public int Sum(TestObject2 obj1, TestObject2 obj2)
            {
                return obj1.Count2 + obj2.Count2;
            }

            public class TestObject1
            {
                public int Count1 { get; set; }
            }

            public class TestObject2
            {
                public int Count2 { get; set; }
            }


            public TestObject1 CreateTestObject1(int count)
            {
                return new TestObject1() { Count1 = count };
            }

            public TestObject2 CreateTestObject2(int count)
            {
                return new TestObject2() { Count2 = count };
            }


        }

        [Theory]
        [InlineData("1+2", 3)]
        [InlineData("1-2", -1)]
        [InlineData("2*2", 4)]
        [InlineData("10/2", 5)]
        [InlineData("7%2", 1)]
        public void ShouldHandleIntegers(string input, int expected)
        {
            var expression = new Expression(input);
            var sut = expression.ToLambda<int>();

            Assert.Equal(sut(), expected);
        }

        [Fact]
        public void ShouldHandleParameters()
        {
            var expression = new Expression("[FieldA] > 5 && [FieldB] = 'test'");
            var sut = expression.ToLambda<Context, bool>();
            var context = new Context { FieldA = 7, FieldB = "test" };

            Assert.True(sut(context));
        }

        [Fact]
        public void ShouldHandleOverloadingSameParamCount()
        {
            var expression = new Expression("Test('Hello', ' world!')");
            var sut = expression.ToLambda<Context, string>();
            var context = new Context();

            Assert.Equal("Hello world!", sut(context));
        }

        [Fact]
        public void ShouldHandleOverloadingDifferentParamCount()
        {
            var expression = new Expression("Test(Test(1, 2), 3, 4)");
            var sut = expression.ToLambda<Context, int>();
            var context = new Context();

            Assert.Equal(10, sut(context));
        }

        [Fact]
        public void ShouldHandleOverloadingObjectParameters()
        {
            var expression = new Expression("Sum(CreateTestObject1(2), CreateTestObject2(2)) + Sum(CreateTestObject2(1), CreateTestObject1(5))");
            var sut = expression.ToLambda<Context, int>();
            var context = new Context();

            Assert.Equal(10, sut(context));
        }


        [Fact]
        public void ShouldHandleParamsKeyword()
        {
            var expression = new Expression("Sum(Test(1,1),2)");
            var sut = expression.ToLambda<Context, int>();
            var context = new Context();

            Assert.Equal(4, sut(context));
        }

        [Fact]
        public void ShouldHandleMixedParamsKeyword()
        {
            var expression = new Expression("Sum('Your total is: ', Test(1,1), 2, 3)");
            var sut = expression.ToLambda<Context, string>();
            var context = new Context();

            Assert.Equal("Your total is: 7", sut(context));
        }

        [Fact]
        public void ThrowUnknownParameter()
        {
            var expression = new Expression("3*PI");
            var action = new Action(() => expression.ToLambda<double>());
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldHandleCustomFunctions()
        {
            var expression = new Expression("Test(Test(1, 2), 3)");
            var sut = expression.ToLambda<Context, int>();
            var context = new Context();

            Assert.Equal(6, sut(context));
        }

        [Fact]
        public void MissingMethod()
        {
            var expression = new Expression("MissingMethod(1)");
            try
            {
                var sut = expression.ToLambda<Context, int>();
            }
            catch (System.MissingMethodException ex)
            {

                System.Diagnostics.Debug.Write(ex);
                Assert.True(true);
                return;
            }

            Assert.True(false);

        }

        [Fact]
        public void ShouldHandleTernaryOperator()
        {
            var expression = new Expression("Test(1, 2) = 3 ? 1 : 2");
            var sut = expression.ToLambda<Context, int>();
            var context = new Context();

            Assert.Equal(1, sut(context));
        }

        [Fact]
        public void Issue1()
        {
            var expr = new Expression("2 + 2 - a - b - x");

            decimal x = 5m;
            decimal a = 6m;
            decimal b = 7m;

            expr.AddParameter("x", x);
            expr.AddParameter("a", a);
            expr.AddParameter("b", b);

            var f = expr.ToLambda<float>(); // Here it throws System.ArgumentNullException. Parameter name: expression
            Assert.Equal(-14, f());
        }

        [Fact]
        public void Issue13Single()
        {
            var expr = new Expression("a + b");
            var index = expr.AddParameter("a", (decimal)1);
            expr.AddParameter("b", (decimal)2);

            var f = expr.ToLambda<float>();
            Assert.Equal(3, f());

            expr.SetParameter(index, (decimal)3);
            Assert.Equal(5, f());
        }

        [Fact]
        public void Issue13Double()
        {
            var expr = new Expression("a + b");
            var index = expr.AddParameter("a", (decimal)1);
            expr.AddParameter("b", (decimal)2);

            var f = expr.ToLambda<double>();
            Assert.Equal(3, f());

            expr.SetParameter(index, (decimal)3);
            Assert.Equal(5, f());
        }

        [Fact]
        public void Issue13Int32()
        {
            var expr = new Expression("a + b");
            var index = expr.AddParameter("a", (decimal)1);
            expr.AddParameter("b", (decimal)2);

            var f = expr.ToLambda<int>();
            Assert.Equal(3, f());

            expr.SetParameter(index, (decimal)3);
            Assert.Equal(5, f());
        }

        [Fact]
        public void Issue13Byte()
        {
            var expr = new Expression("a + b");
            var index = expr.AddParameter("a", (decimal)1);
            expr.AddParameter("b", (decimal)2);

            var f = expr.ToLambda<byte>();
            Assert.Equal(3, f());

            expr.SetParameter(index, (decimal)3);
            Assert.Equal(5, f());
        }

        [Theory]
        [InlineData("if(true, true, false)")]
        [InlineData("in(3, 1, 2, 3, 4)")]
        public void ShouldHandleBuiltInFunctions(string input)
        {
            var expression = new Expression(input);
            var sut = expression.ToLambda<bool>();
            Assert.True(sut());
        }

        [Fact]
        public void ShouldHandleExtendedParameters()
        {
            var expression = new Expression("PI");
            expression.EvaluateParameterExpression += (sender, args) =>
            {
                if (args.Name == "PI")
                {
                    args.Result = LQ.Expression.Constant(3.1415);
                }
            };

            var sut = expression.ToLambda<double>();
            sut().Should().BeApproximately(3.1415, 0.001);
        }

        [Fact]
        public void ShouldHandleExtendedFunctions()
        {
            var expression = new Expression("MyFunc(X1)");
            expression.AddParameter("X1", 1);
            expression.EvaluateFunctionExpression += (sender, args) =>
            {
                if (args.Name == "MyFunc")
                {
                    args.Result = LQ.Expression.Add(args.ArgumentExpressions[0], LQ.Expression.Constant((long)1));
                }
            };

            var sut = expression.ToLambda<int>();
            sut().Should().Be(2);

            expression.SetParameter("X1", 2);
            sut().Should().Be(3);
        }

        [Theory]
        [InlineData("[FieldA] > [FieldC]", true)]
        [InlineData("[FieldC] > 1.34", true)]
        [InlineData("[FieldC] > (1.34 * 2) % 3", false)]
        [InlineData("[FieldE] = 2", true)]
        [InlineData("[FieldD] > 0", false)]
        public void ShouldHandleDataConversions(string input, bool expected)
        {
            var expression = new Expression(input);
            var sut = expression.ToLambda<Context, bool>();
            var context = new Context { FieldA = 7, FieldB = "test", FieldC = 2.4m, FieldE = 2 };

            Assert.Equal(expected, sut(context));
        }

        [Theory]
        [InlineData("Min(3,2)", 2)]
        [InlineData("Min(3.2,6.3)", 3.2)]
        [InlineData("Max(2.6,9.6)", 9.6)]
        [InlineData("Max(9,6)", 9.0)]
        [InlineData("Pow(5,2)", 25)]
        public void ShouldHandleNumericBuiltInFunctions(string input, double expected)
        {
            var expression = new Expression(input);
            var sut = expression.ToLambda<object>();
            Assert.Equal(expected, sut());
        }


        [Fact]
        public void ShouldCalculateLambdaForDifferentParameters()
        {
            var expression = new Expression("a+b");
            expression.AddParameter("a", 1);
            expression.AddParameter("b", 2);

            var sut = expression.ToLambda<int>();
            sut().Should().Be(3);

            expression.SetParameter("a", 2);
            expression.SetParameter("b", 5);
            sut().Should().Be(7);
        }

        [Fact]
        public void ShouldCalculateLambdaForDifferentParametersAddedAfterLambdaCreation()
        {
            var expression = new Expression("a+b");
            expression.AddParameter("a", null);
            expression.AddParameter("b", null);

            var sut = expression.ToLambda<int>();

            expression.SetParameter("a", 2);
            expression.SetParameter("b", 5);

            sut().Should().Be(7);
        }


        [Fact]
        public void ShouldCalculateLambdaForDifferentParametersAddedAfterLambdaCreation_Multiply()
        {
            var expression = new Expression("a*b+b*2");
            expression.AddParameter("a", null);
            expression.AddParameter("b", null);

            var sut = expression.ToLambda<double>();

            expression.SetParameter("a", 2.5);
            expression.SetParameter("b", 4);

            sut().Should().Be(18);
        }


        [Fact]
        public void LambdaWithTypeArgument()
        {
            var expression = new Expression("a*b+b*2");
            expression.AddParameter("a", 2.5);
            expression.AddParameter("b", 4);

            var sut = expression.ToLambda(typeof(double));

            var result = sut.DynamicInvoke();

            result.Should().Be(18);
        }
    }
}