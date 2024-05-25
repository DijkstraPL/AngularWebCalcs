using Build_IT_NCalc.Units.Interfaces;

namespace Build_IT_NCalc.Units.MassUnits
{
    public abstract class MassUnit : Unit, ISubUnit<Kilogram>
    {
        protected MassUnit(string symbol, double power = 1) : base(symbol, power)
        {
        }
    }
}
