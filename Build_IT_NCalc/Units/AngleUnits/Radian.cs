using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.AngleUnits
{
     public class Radian : AngleUnit, IMainUnit
    {
        public const string Unit = "rad";

        public Radian() : base(Unit)
        {
        }
        public Radian(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Radian>(valueUnit, val => val );
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Radian>(valueUnit, val => val);
        }
    }
}