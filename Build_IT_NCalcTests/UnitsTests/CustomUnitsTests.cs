using Build_IT_NCalc.Units;
using Build_IT_NCalc.Units.ForceUnits;
using Build_IT_NCalc.Units.Interfaces;
using NCalc;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.UnitsTests
{
    public class CustomUnitsTests
    {
        [Fact]
        public void OwnCustomUnits()
        {
            var expr = new Expression("a+b", EvaluateOptions.AllowUnitCalculations);

            expr.RegisterUnit<MainCustomUnit>();
            expr.RegisterUnit<SubCustomUnit>();

            expr.AddParameter("a", expr.ValueUnit(1, "Cst"));
            expr.AddParameter("b", expr.ValueUnit(22, "SubCst"));

            var result = expr.Evaluate();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(3, ((ValueUnit)result).Value);
            Assert.Contains("Cst", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        public class MainCustomUnit : Unit, IMainUnit
        {
            public MainCustomUnit() : base("Cst")
            {
            }

            public MainCustomUnit(double power = 1) : base("Cst", power)
            {
            }

            public override void TransformFromMain(ValueUnit valueUnit)
            {
                TransformTo<MainCustomUnit>(valueUnit, val => val);
            }

            public override void TransformToMain(ValueUnit valueUnit)
            {
                TransformTo<MainCustomUnit>(valueUnit, val => val);
            }
        }

        public class SubCustomUnit : Unit, ISubUnit<MainCustomUnit>
        {
            public SubCustomUnit() : base("SubCst")
            {
            }

            public SubCustomUnit(double power = 1) : base("SubCst", power)
            {
            }

            public override void TransformFromMain(ValueUnit valueUnit)
            {
                TransformTo<SubCustomUnit>(valueUnit, val => val * 11);
            }

            public override void TransformToMain(ValueUnit valueUnit)
            {
                TransformTo<MainCustomUnit>(valueUnit, val => val / 11);
            }
        }
    }
}
