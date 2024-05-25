using Build_IT_NCalc.Units;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class TemperatureUnitsTests
    {
        [Theory]
        [InlineData(1, "Δ°C", 1)]
        [InlineData(1, "°C", 274.15)]
        public void ConvertToKelwinsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  unit);

            angleUnits.TransformTo("K", angleUnits[unit]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "Δ°C", 1)]
        [InlineData(1, "°C", -272.15)]
        public void ConvertFromKelwinsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  "K");

            angleUnits.TransformTo(unit, angleUnits["K"]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
