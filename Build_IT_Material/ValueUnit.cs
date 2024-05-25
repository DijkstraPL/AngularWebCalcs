using System;

namespace Build_IT_Material
{
    public class ValueUnit
    {

        public double Value { get; set; }
        public string Symbol { get; }
        public string Unit { get; }

        public ValueUnit(string symbol, string unit)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException($"'{nameof(symbol)}' cannot be null or whitespace.", nameof(symbol));

            Symbol = symbol;
            Unit = unit;
        }
    }
}
