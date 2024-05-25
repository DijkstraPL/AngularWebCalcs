using Build_IT_NCalc.Units;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class PressureUnitsTests
    {
        [Theory]
        [InlineData(1, "GPa", 1000000)]
        [InlineData(1, "MPa", 1000)]
        [InlineData(1, "Pa", 0.001)]
        public void ConvertToKiloPascalsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value, unit);

            angleUnits.TransformTo("kPa", angleUnits[unit]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "GPa", 0.000001)]
        [InlineData(1, "MPa", 0.001)]
        [InlineData(1, "Pa", 1000)]
        public void ConvertFromKiloPascalsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value, "kPa");

            angleUnits.TransformTo(unit, angleUnits["kPa"]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
