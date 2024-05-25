using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.ForceUnits
{
    public class MegaNewton : Unit, ISubUnit<KiloNewton>
    {
        public const string Unit = "MN";

        public MegaNewton() : base( Unit)
        {
        }
        public MegaNewton(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloNewton>(valueUnit, val => val * GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<MegaNewton>(valueUnit, val => val / GetMultiplier(1000));
        }
    }
}
