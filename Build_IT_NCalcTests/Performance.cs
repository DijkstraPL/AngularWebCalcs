using Build_IT_NCalc.Units;
using System;
using System.Diagnostics;
using Xunit;

namespace NCalc.Tests
{
    public class Performance
    {
        private const int Iterations = 100000;

        private class Context
        {
            public int Param1 { get; set; }
            public int Param2 { get; set; }

            public int Foo(int a, int b)
            {
                return Math.Min(a, b);
            }
        }

        [Theory]
        [InlineData("(4 * 12 / 7) + ((9 * 2) % 8)")]
        [InlineData("5 * 2 = 2 * 5 && (1 / 3.0) * 3 = 1")]
        public void Arithmetics(string formula)
        {
            var expression = new Expression(formula);
            var lambda = expression.ToLambda<object>();

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => lambda());

            PrintResult(formula, m1, m2);
        }

        [Theory]
        [InlineData("[Param1] * 7 + [Param2]")]
        public void ParameterAccess(string formula)
        {
            var expression = new Expression(formula);
            var lambda = expression.ToLambda<Context, int>();

            var context = new Context {Param1 = 4, Param2 = 9};
            expression.AddParameter("Param1", 4);
            expression.AddParameter("Param2", 9);

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => lambda(context));

            PrintResult(formula, m1, m2);
        }

        [Theory]
        [InlineData("[Param1] * 7 + [Param2]")]
        public void DynamicParameterAccess(string formula)
        {
            var expression = new Expression(formula);
            var lambda = expression.ToLambda<Context, int>();

            var context = new Context { Param1 = 4, Param2 = 9 };
            expression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = context.Param1;
                if (name == "Param2") args.Result = context.Param2;
            };

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => lambda(context));

            PrintResult(formula, m1, m2);
        }

        [Theory]
        [InlineData("Foo([Param1] * 7, [Param2])")]
        public void FunctionWithDynamicParameterAccess(string formula)
        {
            var expression = new Expression(formula);
            var lambda = expression.ToLambda<Context, int>();

            var context = new Context { Param1 = 4, Param2 = 9 };
            expression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = context.Param1;
                if (name == "Param2") args.Result = context.Param2;
            };
            expression.EvaluateFunction += (name, args) =>
            {
                if (name == "Foo")
                {
                    var param = args.EvaluateParameters();
                    args.Result = context.Foo((int) param[0], (int) param[1]);
                }
            };

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => lambda(context));

            PrintResult(formula, m1, m2);
        }

        [Theory]
        [InlineData("Max([Param1] * 7, [Param2])")]
        public void FunctionWithUnitOfMeasures(string formula)
        {
            var random = new Random();

            var expression = new Expression(formula);
            expression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = random.Next();
                if (name == "Param2") args.Result = random.Next();
            };

            var unitExpression = new Expression(formula, EvaluateOptions.AllowUnitCalculations);
            unitExpression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = unitExpression.ValueUnit(random.Next(), "mm");
                if (name == "Param2") args.Result = unitExpression.ValueUnit(random.Next(), "m");
            };

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => unitExpression.Evaluate());

            PrintResult(formula, m1, m2);
        }

        [Theory]
        [InlineData("[Param1] * [Param2] / (3 * [Param3]) + [Param4]")]
        public void FunctionWithUnitOfMeasures2(string formula)
        {
            var random = new Random();

            var expression = new Expression(formula);

            expression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = random.Next();
                if (name == "Param2") args.Result = random.Next();
                if (name == "Param3") args.Result = random.Next();
                if (name == "Param4") args.Result = random.Next();
            };

            var unitExpression = new Expression(formula, EvaluateOptions.AllowUnitCalculations);

            unitExpression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = unitExpression.ValueUnit(random.Next(), "kg");
                if (name == "Param2") args.Result = unitExpression.ValueUnit(random.Next(), "m");
                if (name == "Param3") args.Result = unitExpression.ValueUnit(random.Next(), "s", "s");
                if (name == "Param4") args.Result = unitExpression.ValueUnit(random.Next(), "kN");
            };

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => unitExpression.Evaluate());

            PrintResult(formula, m1, m2);
        }

        [Theory]
        [InlineData("[Param1] * [Param2] / (3 * [Param3]) + [Param4]")]
        public void FunctionWithUnitOfMeasures3(string formula)
        {
            var random = new Random();

            var expression = new Expression(formula);

            expression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = random.Next();
                if (name == "Param2") args.Result = random.Next();
                if (name == "Param3") args.Result = random.Next();
                if (name == "Param4") args.Result = random.Next();
            };

            var unitExpression = new Expression(formula, EvaluateOptions.AllowUnitCalculations);

            unitExpression.EvaluateParameter += (name, args) =>
            {
                if (name == "Param1") args.Result = unitExpression.ValueUnit(random.Next(), "km");
                if (name == "Param2") args.Result = unitExpression.ValueUnit(random.Next(), "m");
                if (name == "Param3") args.Result = unitExpression.ValueUnit(random.Next(), "cm");
                if (name == "Param4") args.Result = unitExpression.ValueUnit(random.Next(), "dm");
            };

            var m1 = Measure(() => expression.Evaluate());
            var m2 = Measure(() => unitExpression.Evaluate());

            PrintResult(formula, m1, m2);
        }

        private TimeSpan Measure(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < Iterations; i++)
                action();
            sw.Stop();
            return sw.Elapsed;
        }

        private static void PrintResult(string formula, TimeSpan m1, TimeSpan m2)
        {
            Debug.WriteLine(new string('-', 60));
            Debug.WriteLine("Formula: {0}", formula);
            Debug.WriteLine("Expression: {0:N} evaluations / sec", Iterations / m1.TotalSeconds);
            Debug.WriteLine("Lambda: {0:N} evaluations / sec", Iterations / m2.TotalSeconds);
            Debug.WriteLine("Lambda Speedup: {0:P}%", (Iterations / m2.TotalSeconds) / (Iterations / m1.TotalSeconds) - 1);
            Debug.WriteLine(new string('-', 60));
        }
    }
}
