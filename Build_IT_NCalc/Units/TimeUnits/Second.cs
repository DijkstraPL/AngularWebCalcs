using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.TimeUnits
{
    public class Second : Unit, ISubUnit<Hour>
    {
        public const string Unit = "s";

        public Second() : base(Unit)
        {
        }
        public Second(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Second>(valueUnit, val => val * GetMultiplier(3600));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Hour>(valueUnit, val => val / GetMultiplier(3600));
        }
    }
}
