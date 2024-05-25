using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.TemperatureUnits
{
    public class CelsiusDifference : Unit, ISubUnit<Kelwin>
    {
        public const string Unit = "Δ°C";

        public CelsiusDifference() : base(Unit)
        {
        }
        public CelsiusDifference(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            valueUnit.ReplaceUnit(this, valueUnit.Value, new CelsiusDifference());
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            valueUnit.ReplaceUnit(this, valueUnit.Value, new Kelwin());
        }
    }
}
