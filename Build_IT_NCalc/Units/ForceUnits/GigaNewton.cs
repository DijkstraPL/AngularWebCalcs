using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.ForceUnits
{
    public class GigaNewton : Unit, ISubUnit<KiloNewton>
    {
        public const string Unit = "GN";

        public GigaNewton() : base(Unit)
        {
        }
        public GigaNewton(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloNewton>(valueUnit, val => val * GetMultiplier(1000000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<GigaNewton>(valueUnit, val => val / GetMultiplier(1000000));
        }
    }
}
