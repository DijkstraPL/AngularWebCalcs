using System;
using System.Text;

namespace Build_IT_NCalc.Units.Interfaces
{
    public interface ISubUnit<T> : IUnit where T:Unit, IMainUnit, new()
    {
        void TransformToMain(ValueUnit valueUnit);
        void TransformFromMain(ValueUnit valueUnit);
    }
}
