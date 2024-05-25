using Build_IT_NCalc.Units;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class LengthUnitsTests
    {
        [Theory]
        [InlineData(1, "km", 1000)]
        [InlineData(1, "dm", 0.1)]
        [InlineData(1, "cm", 0.01)]
        [InlineData(1, "mm", 0.001)]
        public void ConvertToMetersTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  unit);

            angleUnits.TransformTo("m", angleUnits[unit]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "km", 0.001)]
        [InlineData(1, "dm", 10)]
        [InlineData(1, "cm", 100)]
        [InlineData(1, "mm", 1000)]
        public void ConvertFromMetersTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  "m");

            angleUnits.TransformTo(unit, angleUnits["m"]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
