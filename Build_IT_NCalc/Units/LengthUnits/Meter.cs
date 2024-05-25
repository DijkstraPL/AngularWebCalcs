using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.LengthUnits
{
    public class Meter : Unit, IMainUnit, ISubUnit<Meter>
    {
        public const string Unit = "m";

        public Meter() : base(Unit)
        {
        }
        public Meter(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Meter>(valueUnit, val => val);
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Meter>(valueUnit, val => val);
        }
    }
}
