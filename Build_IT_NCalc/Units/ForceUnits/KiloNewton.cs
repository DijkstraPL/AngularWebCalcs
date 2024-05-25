using Build_IT_NCalc.Units.Interfaces;
using Build_IT_NCalc.Units.LengthUnits;
using Build_IT_NCalc.Units.MassUnits;
using Build_IT_NCalc.Units.PressureUnits;
using Build_IT_NCalc.Units.TimeUnits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_NCalc.Units.ForceUnits
{
    public class KiloNewton : Unit, IMainUnit, ISubUnit<KiloNewton>
    {
        public const string Unit = "kN";

        public KiloNewton() : base(Unit)
        {
        }
        public KiloNewton(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloNewton>(valueUnit, val => val);
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<KiloNewton>(valueUnit, val => val);
        }

        //public ValueUnit Compose(ValueUnit valueUnit)
        //{
        //    var kilograms = valueUnit.Units.Where(u => u.Symbol == Kilogram.Unit);
        //    var meters = valueUnit.Units.Where(u => u.Symbol == Meter.Unit);
        //    var hours = valueUnit.Units.Where(u => u.Symbol == Hour.Unit);

        //    var kilogram = kilograms.FirstOrDefault(k => meters.Any(m => m.Power == k.Power) && hours.Any(h => h.Power == k.Power * -2));
        //    var meter = meters.FirstOrDefault(m => m.Power == kilogram?.Power && hours.Any(h => h.Power == m.Power * -2));
        //    var hour = hours.FirstOrDefault(h => h.Power == kilogram?.Power * -2 && h.Power == meter?.Power * -2);
        //    if (kilogram is not null && meter is not null && hour is not null)
        //    {
        //        valueUnit.ReplaceUnit(kilogram, valueUnit.Value / 1000, new KiloNewton(kilogram.Power))
        //            .ReplaceUnit(meter, valueUnit.Value, null)
        //            .ReplaceUnit(hour, valueUnit.Value * Math.Pow(3600, hour.Power), null);
        //    }


        //    var kiloPascals = valueUnit.Units.Where(u => u.Symbol == KiloPascal.Unit);
        //    var meter2 = valueUnit.Units.FirstOrDefault(u => u.Symbol == Meter.Unit && kiloPascals.Any(k => k.Power * 2 == u.Power));
        //    var kiloPascal = kiloPascals.FirstOrDefault(k => k.Power * 2 == meter2?.Power);
        //    if (kiloPascal is not null && meter2 is not null)
        //    {
        //        valueUnit.ReplaceUnit(kiloPascal, valueUnit.Value, new KiloNewton(kiloPascal.Power))
        //            .ReplaceUnit(meter2, valueUnit.Value, null);
        //    }

        //    return valueUnit;
        //}
        internal override bool CanDecompose() => true;

        internal override bool Decompose(ValueUnit valueUnit)
        {
            if (valueUnit.Units.Contains(this))
            {
                valueUnit.ReplaceUnit(this, valueUnit.Value * 1000, new Kilogram(1 * Power))
                     .ReplaceUnit(null, valueUnit.Value, new Meter(1 * Power))
                     .ReplaceUnit(null, valueUnit.Value, new Second(-2 * Power));
                return true;
            }
            return false;
        }
        internal override IEnumerable<Unit> Decompose()
        {
            yield return new Kilogram(1*Power);
            yield return new Meter(1 * Power);
            yield return new Second(-2 * Power);
        }
    }
}
