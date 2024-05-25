using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.TimeUnits
{
    public class Day : Unit, ISubUnit<Hour>
    {
        public const string Unit = "day";

        public Day() : base(Unit)
        {
        }
        public Day(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Minute>(valueUnit, val => val / GetMultiplier(24));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Hour>(valueUnit, val => val * GetMultiplier(24));
        }
    }
}
