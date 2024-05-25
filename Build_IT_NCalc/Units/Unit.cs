using Build_IT_NCalc.Units.ForceUnits;
using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Build_IT_NCalc.Units
{
    public abstract class Unit : IUnit, IEquatable<Unit>, IComparable<Unit>
    {
        public string Symbol { get; }
        public double Power { get; internal set; }

        protected Unit(string symbol, double power = 1)
        {
            Symbol = symbol;
            Power = power;
        }

        public Unit Copy(double? power = null)
        {
            var type = this.GetType();
            var newUnit = (Unit)Activator.CreateInstance(type);
            newUnit.Power = power ?? Power;
            return newUnit;
        }

        public abstract void TransformToMain(ValueUnit valueUnit);
        public abstract void TransformFromMain(ValueUnit valueUnit);

        protected virtual double GetMultiplier(double multiplier)
            => Math.Pow(multiplier, Power);

        protected void TransformTo<T>(ValueUnit valueUnit, Func<double, double> transformation) where T : Unit, new()
        {
            var newUnit = new T();
            newUnit.Power = Power;
            valueUnit.ReplaceUnit(this, transformation(valueUnit.Value), newUnit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj ,null))
                return false;

            if (obj is Unit other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode() * 7 + Power.GetHashCode() * 11;
        }

        public bool Equals([AllowNull] Unit other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return this.Symbol == other.Symbol && 
                this.Power == other.Power;
        }

        public override string ToString()
        {
            if (Power != 1)
                return Symbol + "^" + Power;
            return Symbol;
        }

        public int CompareTo([AllowNull] Unit other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException(nameof(other));

            var result = Symbol.CompareTo(other.Symbol);
            if (result == 0 || Power < 0 && other.Power > 0)
                return -Power.CompareTo(other.Power);
            return result;
        }

        internal virtual bool CanDecompose() => false;

        internal virtual bool Decompose(ValueUnit valueUnit)
        {
            return false;
        }
        internal virtual IEnumerable<Unit> Decompose()
        {
            yield return this;
        }
    }
}
