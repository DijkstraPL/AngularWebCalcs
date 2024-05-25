using Build_IT_NCalc.Units.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.TemperatureUnits
{
    public class Kelwin : Unit, IMainUnit, ISubUnit<Kelwin>
    {
        public const string Unit = "K";

        public Kelwin() : base(Unit)
        {
        }
        public Kelwin(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            valueUnit.ReplaceUnit(this, valueUnit.Value, new Kelwin());
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            valueUnit.ReplaceUnit(this, valueUnit.Value, new Kelwin());
        }
    }
}
