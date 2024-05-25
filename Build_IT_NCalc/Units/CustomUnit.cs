using System;
using System.Linq;

namespace Build_IT_NCalc.Units
{
    public class CustomUnit : Unit
    {
        public CustomUnit(string symbol, double power = 1) : base(symbol, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            var unit = GetUnit();
            if (unit is not null)
                unit.TransformFromMain(valueUnit);
            else
                valueUnit.ReplaceUnit(this, valueUnit.Value, new CustomUnit(Symbol, Power));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            var unit = GetUnit();
            if (unit is not null)
                unit.TransformToMain(valueUnit);
            else
                valueUnit.ReplaceUnit(this, valueUnit.Value, new CustomUnit(Symbol, Power));
        }

        private Unit GetUnit()
        {
            var unitFunc = UnitRegistry.Instance.RegisteredUnits.FirstOrDefault(ru => ru.Key == Symbol).Value;
            if (unitFunc is null)
                return null;

            var unit = unitFunc(Power);
            if (unit is null)
                return null;
            return unit;
        }
    }
}
