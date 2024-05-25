using Build_IT_NCalc.Units.Interfaces;
using System;

namespace Build_IT_NCalc.Units.TemperatureUnits
{
    public class Celsius : Unit, ISubUnit<Kelwin>
    {
        public const string Unit = "°C";

        public Celsius() : base(Unit)
        {
        }
        public Celsius(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            if (Power != 1)
                throw new ArgumentException(paramName: nameof(Power), message: "Parameter couldn't be transform due to power != 1");

            TransformTo<Celsius>(valueUnit, val => val - 273.15);
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            if (Power != 1)
                throw new ArgumentException(paramName: nameof(Power), message: "Parameter couldn't be transform due to power != 1");

            TransformTo<Kelwin>(valueUnit, val => val + 273.15);
        }
    }
}
