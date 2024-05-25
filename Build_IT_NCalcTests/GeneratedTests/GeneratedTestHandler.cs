using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Build_IT_NCalcTests.GeneratedTests
{
    public class GeneratedTestHandler
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetDataFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void TestNotLambda(string equation, string expectedResult, params object[] parameters)
        {
            var unitExpression = new Expression(equation, EvaluateOptions.AllowUnitCalculations);

            foreach (TestDataInputParameter parameter in parameters)
                unitExpression.AddParameter(parameter.Name, parameter.Value);

            var result = unitExpression.Evaluate();
            if (result is ValueUnit valueUnit)
            {
                valueUnit.OrganizeUnits();
                result = valueUnit.ToString(CultureInfo.InvariantCulture);
            }

            Assert.Equal(expectedResult, result.ToString());
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetDataFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void TestLambda(string equation, string expectedResult, params object[] parameters)
        {
            var unitExpression = new Expression(equation, EvaluateOptions.AllowUnitCalculations);

            foreach (TestDataInputParameter parameter in parameters)
                unitExpression.AddParameter(parameter.Name, parameter.Value);

            var sut = unitExpression.ToLambda<object>();
            var result = sut();
            if (result is ValueUnit valueUnit)
            {
                valueUnit.OrganizeUnits();
                result = valueUnit.ToString(CultureInfo.InvariantCulture);
            }

            Assert.Equal(expectedResult, result.ToString());
        }
    }
}
