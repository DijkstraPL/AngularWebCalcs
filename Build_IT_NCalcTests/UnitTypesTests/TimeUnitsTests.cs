using Build_IT_NCalc.Units;
using Build_IT_NCalc.Units.TimeUnits;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.UnitTypesTests
{
    public class TimeUnitsTests
    {
        [Theory]
        [InlineData(1, "day", 24)]
        [InlineData(120, "min", 2)]
        [InlineData(7200, "s", 2)]
        public void ConvertToHoursTest(double value, string unit, double expectedResult)
        {
            var time = new ValueUnit(value,  unit);

            time.TransformTo<Hour>(time.Units.First());

            Assert.Equal(expectedResult, time.Value, 3);
        }

        [Theory]
        [InlineData(12, "day", 0.5)]
        [InlineData(1, "min", 60)]
        [InlineData(1, "s", 3600)]
        public void ConvertFromHoursTest(double value, string unit, double expectedResult)
        {
            var time = new ValueUnit(value, "hr");

            time.TransformTo(unit, time.Units.First());

            Assert.Equal(expectedResult, time.Value, 3);
        }
    }
}
