using Build_IT_NCalc.Units;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class PowerUnitsTests
    {
        [Theory]
        [InlineData(1, "MW", 1000)]
        [InlineData(1, "W", 0.001)]
        public void ConvertToKilowatsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  unit);

            angleUnits.TransformTo("kW", angleUnits[unit]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "MW", 0.001)]
        [InlineData(1, "W", 1000)]
        public void ConvertFromKilowatsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  "kW");

            angleUnits.TransformTo(unit, angleUnits["kW"]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
