using Build_IT_NCalc.Units;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class AngleUnitsTests
    {
        [Theory]
        [InlineData(1, "rad", 1)]
        [InlineData(180, "deg", 3.14159265359)]
        [InlineData(200, "grad", 3.14159265359)]
        public void ConvertToRadiansTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value, unit);

            angleUnits.TransformTo("rad", angleUnits.Units.First());

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }

        [Theory]
        [InlineData(1, "rad", 1)]
        [InlineData(3.14159265359, "deg", 180)]
        [InlineData(3.14159265359, "grad", 200)]
        public void ConvertFromRadiansTest(double value, string unit, double expectedResult)
        {
            var angleUnits = new ValueUnit(value,  "rad");

            angleUnits.TransformTo(unit, angleUnits.Units.First());

            Assert.Equal(expectedResult, angleUnits.Value, 3);
        }
    }
}
