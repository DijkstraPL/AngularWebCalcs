using Build_IT_NCalc.Units;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class MassUnitsTests
    {
        [Theory]
        [InlineData(1, "t", 1000)]
        public void ConvertToKilogramsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  unit);

            angleUnits.TransformTo("kg", angleUnits[unit]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "t", 0.001)]
        public void ConvertFromKilogramsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  "kg");

            angleUnits.TransformTo(unit, angleUnits["kg"]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
