using Build_IT_NCalc.Units;

namespace Build_IT_NCalcTests.GeneratedTests
{
    public class TestDataInputParameter
    {
        public string Name { get; }
        public object Value { get; }

        public TestDataInputParameter(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public TestDataInputParameter(string name, double value, string units)
        {
            Name = name;
            Value = new ValueUnit(value, units.Split(',', System.StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
