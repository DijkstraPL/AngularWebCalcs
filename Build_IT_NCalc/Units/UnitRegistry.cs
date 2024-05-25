using Build_IT_NCalc.Units.AngleUnits;
using Build_IT_NCalc.Units.ForceUnits;
using Build_IT_NCalc.Units.Interfaces;
using Build_IT_NCalc.Units.LengthUnits;
using Build_IT_NCalc.Units.MassUnits;
using Build_IT_NCalc.Units.PowerUnits;
using Build_IT_NCalc.Units.PressureUnits;
using Build_IT_NCalc.Units.TemperatureUnits;
using Build_IT_NCalc.Units.TimeUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Build_IT_NCalc.Units
{
    using CanCompose = Func<IEnumerable<Unit>, bool>;
    using Compose = Func<ValueUnit, ValueUnit>;

    public class UnitRegistry : IDisposable
    {
        public static UnitRegistry Instance { get; private set; } = new UnitRegistry();

        public IDictionary<string, Func<double, Unit>> RegisteredUnits { get; } = new Dictionary<string, Func<double, Unit>>();
        public IEnumerable<string> UnitNames => RegisteredUnits.Select(ru => ru.Key);

        public List<(CanCompose canCompose, Compose compose)> RegisteredCompositions { get; } = new();

        private UnitRegistry()
        {
            Register<Degree>();
            Register<Degree2>();
            Register<Gradian>();
            Register<Radian>();

            Register<GigaNewton>();
            Register<MegaNewton>();
            Register<KiloNewton>();
            Register<Newton>();

            Register<Meter>();
            Register<Centimeter>();
            Register<Decimeter>();
            Register<Kilometer>();
            Register<Milimeter>();

            Register<Kilogram>();
            Register<Tonne>();

            Register<KiloWatt>();
            Register<MegaWatt>();
            Register<Watt>();

            Register<GigaPascal>();
            Register<MegaPascal>();
            Register<KiloPascal>();
            Register<Pascal>();

            Register<Celsius>();
            Register<CelsiusDifference>();
            Register<Kelwin>();

            Register<Day>();
            Register<Hour>();
            Register<Minute>();
            Register<Second>();

            RegisterComposition<KiloNewton, Kilogram, Meter, Hour>(val => val / 1000 / 3600 / 3600, 1, 1,-2);
            RegisterComposition<KiloPascal, KiloNewton, Meter>(val => val,1, -2);
            RegisterComposition<KiloNewton, KiloPascal, Meter>(val => val,1, 2);
        }

        public void RegisterComposition<Target, Unit1>(Func<double, double> valueTransform, int power)
            where Target : Unit, IMainUnit, new()
            where Unit1 : Unit, IMainUnit, new()
        {
            RegisterComposition<Target>(valueTransform, (typeof(Unit1), power));
        }
        public void RegisterComposition<Target, Unit1, Unit2>(Func<double, double> valueTransform, int power1, int power2)
            where Target : Unit, IMainUnit, new()
            where Unit1 : Unit, IMainUnit, new()
            where Unit2 : Unit, IMainUnit, new()
        {
            RegisterComposition<Target>(valueTransform, (typeof(Unit1), power1), (typeof(Unit2), power2));
        }
        public void RegisterComposition<Target, Unit1, Unit2, Unit3>(Func<double, double> valueTransform, int power1, int power2, int power3)
            where Target : Unit, IMainUnit, new()
            where Unit1 : Unit, IMainUnit, new()
            where Unit2 : Unit, IMainUnit, new()
            where Unit3 : Unit, IMainUnit, new()
        {
            RegisterComposition<Target>(valueTransform, (typeof(Unit1), power1), (typeof(Unit2), power2), (typeof(Unit3), power3));
        }

        private void RegisterComposition<Target>(Func<double, double> valueTransform, params (Type unitType, int power)[] units)
            where Target : Unit, IMainUnit, new()
        {
            CanCompose canCompose = valueUnit =>
            {
                return units.All(ut => valueUnit
                                .Any(u => u.GetType() == ut.unitType && u.Power == ut.power));
            };

            Compose compose = vu =>
            {
                List<Unit> oldUnits = new();
                foreach (var unit in units)
                    oldUnits.Add((Unit)Activator.CreateInstance(unit.unitType, unit.power));

                return vu.ReplaceUnits(oldUnits, valueTransform(vu.Value), new Target());
            };
            RegisteredCompositions.Add((canCompose, compose));
        }

        public void Register<NewUnit>() where NewUnit : Unit, new()
        {
            NewUnit unit = new();
            RegisteredUnits.Add(unit.Symbol, power =>
            {
                NewUnit unit = new();
                unit.Power = power;
                return unit;
            });
        }

        internal void Register(CustomUnit customUnit)
        {
            RegisteredUnits.Add(customUnit.Symbol, power =>
            {
                customUnit.Power = power;
                return new CustomUnit(customUnit.Symbol, power);
            });
        }

        internal bool Compose(ValueUnit valueUnit)
        {
            bool composed = false;
            foreach (var composition in RegisteredCompositions)
            {
                if (composition.canCompose(valueUnit.Units))
                {
                    valueUnit = composition.compose(valueUnit);
                    composed = true;
                }
            }
            return composed;
        }

        public void Dispose()
        {
            RegisteredCompositions.Clear();
            RegisteredUnits.Clear();
        }

        internal static void SetInstance(UnitRegistry unitRegistry)
        {
            Instance = unitRegistry ?? throw new ArgumentNullException(nameof(unitRegistry));  
        }
    }
}
