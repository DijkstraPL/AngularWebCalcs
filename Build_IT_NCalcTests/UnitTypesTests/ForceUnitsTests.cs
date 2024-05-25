using Build_IT_NCalc.Units;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class ForceUnitsTests
    {
        [Theory]
        [InlineData(1, "GN", 1000000)]
        [InlineData(1, "MN", 1000)]
        [InlineData(1, "N", 0.001)]
        public void ConvertToKiloNewtonsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value, unit);

            angleUnits.TransformTo("kN", angleUnits[unit]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "GN", 0.000001)]
        [InlineData(1, "MN", 0.001)]
        [InlineData(1, "N", 1000)]
        public void ConvertFromKiloNewtonsTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  "kN");

            angleUnits.TransformTo(unit, angleUnits["kN"]);

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
