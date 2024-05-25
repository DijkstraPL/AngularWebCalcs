using Build_IT_NCalc.Units.ForceUnits;
using Build_IT_NCalc.Units.Interfaces;
using Build_IT_NCalc.Units.LengthUnits;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_NCalc.Units.PressureUnits
{
    public class KiloPascal : Unit, IMainUnit, ISubUnit<KiloPascal>
    {
        public const string Unit = "kPa";

        public KiloPascal() : base(Unit)
        {
        }
        public KiloPascal(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloPascal>(valueUnit, val => val);
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<KiloPascal>(valueUnit, val => val);
        }

        internal override bool Decompose(ValueUnit valueUnit)
        {
            if (valueUnit.Units.Contains(this))
            {
                valueUnit.ReplaceUnit(this, valueUnit.Value, new KiloNewton(1 * Power))
                         .ReplaceUnit(null, valueUnit.Value, new Meter(-2 * Power));
                return true;
            }
            return false;
        }
        internal override bool CanDecompose() => true;

        internal override IEnumerable<Unit> Decompose()
        {
            yield return new KiloNewton(1 * Power);
            yield return new Meter(-2 * Power);
        }
    }
}
