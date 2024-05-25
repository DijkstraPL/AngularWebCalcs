using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Channels;

namespace Build_IT_NCalc.Units
{
    public class ValueUnit : IComparable<ValueUnit>, IComparable, IConvertible, IEquatable<ValueUnit>
    {
        public double Value { get; private set; }
        private List<Unit> _units;
        public IEnumerable<Unit> Units => _units;

        public bool HasUnits => _units.Count > 0;

        public Unit this[string name]
        {
            get => Units.FirstOrDefault(u => u.Symbol == name);
        }

        public ValueUnit(double value) : this(value, new Unit[] { })
        {
        }

        public ValueUnit(double value, params Unit[] units)
        {
            Value = value;
            _units = units.ToList();

            var organizedUnits = new List<Unit>();
            foreach (var unit in Units)
            {
                if (unit is null)
                    continue;

                if (UnitRegistry.Instance.RegisteredUnits.ContainsKey(unit.Symbol))
                    organizedUnits.Add(UnitRegistry.Instance.RegisteredUnits[unit.Symbol](unit.Power));
                else
                {
                    UnitRegistry.Instance.Register(new CustomUnit(unit.Symbol, unit.Power));
                    organizedUnits.Add(UnitRegistry.Instance.RegisteredUnits[unit.Symbol](unit.Power));
                }
            }
            _units.Clear();
            _units.AddRange(organizedUnits);
        }

        public ValueUnit(double value, params string[] units)
        {
            Value = value;
            _units = new List<Unit>();

            foreach (var unit in units)
            {
                if (unit is null)
                    continue;
                var name = unit.Split('^')[0];
                var powerText = unit.Split('^').Length == 2 ? unit.Split('^')[1] : "1";

                if (!Double.TryParse(powerText, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out double power))
                    continue;

                _units.Add(new CustomUnit(name, power));
            }

            var organizedUnits = new List<Unit>();
            foreach (var unit in Units)
            {
                if (UnitRegistry.Instance.RegisteredUnits.ContainsKey(unit.Symbol))
                    organizedUnits.Add(UnitRegistry.Instance.RegisteredUnits[unit.Symbol](unit.Power));
                else
                {
                    UnitRegistry.Instance.Register(new CustomUnit(unit.Symbol, unit.Power));
                    organizedUnits.Add(UnitRegistry.Instance.RegisteredUnits[unit.Symbol](unit.Power));
                }
            }
            _units.Clear();
            _units.AddRange(organizedUnits);
        }

        public void Transform<From, To>()
            where From : Unit, new()
            where To : Unit, new()
        {
            var units = new List<Unit>(Units.Where(u => u.GetType() == typeof(From)));
            foreach (var unit in units)
                TransformTo<To>(unit);
        }

        public void TransformTo(string newUnit, Unit unitToTransform)
        {
            if (!Units.Contains(unitToTransform))
                throw new ArgumentException(paramName: nameof(unitToTransform), message: "Value doesn't contain unit to transform.");
            if (!UnitRegistry.Instance.RegisteredUnits.ContainsKey(newUnit))
                throw new ArgumentException(paramName: nameof(newUnit), message: "No unit registered.");
            unitToTransform.TransformToMain(this);
            var unit = UnitRegistry.Instance.RegisteredUnits[newUnit](unitToTransform.Power);
            unit.Power = unitToTransform.Power;
            unit.TransformFromMain(this);
        }

        public void TransformTo<T>(Type newUnitType, Unit unitToTransform) where T : Unit, new()
        {
            TransformTo<T>(unitToTransform);
        }

        public void RoundValue(double precision)
        {
            Value = Math.Round(Value / precision) * precision;
        }

        public void TransformTo<T>(Unit unitToTransform) where T : Unit, new()
        {
            if (!Units.Contains(unitToTransform))
                throw new ArgumentException(paramName: nameof(unitToTransform), message: "Value doesn't contain unit to transform.");
            unitToTransform.TransformToMain(this);
            var newUnit = new T();
            newUnit.Power = unitToTransform.Power;
            newUnit.TransformFromMain(this);
        }

        public void OrganizeUnits()
        {
            var units = new List<Unit>(Units);
            foreach (var unit in units)
            {
                unit.TransformToMain(this);
            }

            var groupedUnits = Units.GroupBy(u => u.Symbol);
            var newUnits = new List<Unit>();
            foreach (var group in groupedUnits)
            {
                var newUnit = group.First();
                newUnit.Power = group.Sum(g => g.Power);
                if (newUnit.Power != 0)
                    newUnits.Add(newUnit);
            }
            _units = newUnits;
            if (ComposeUnits())
                OrganizeUnits();
        }

        internal bool ComposeUnits()
        {
            return UnitRegistry.Instance.Compose(this);
        }

        internal void DecomposeUnits()
        {
            var units = new List<Unit>(Units);
            foreach (var unit in units)
            {
                unit.TransformToMain(this);
                if (unit.Decompose(this))
                    return;
            }
        }

        internal ValueUnit ReplaceUnit(Unit oldUnit, double newValue, Unit newUnit)
        {
            if (oldUnit is not null)
                _units.Remove(oldUnit);
            if (newUnit is not null)
                _units.Add(newUnit);
            Value = newValue;
            return this;
        }
        internal ValueUnit ReplaceUnit(Unit oldUnit, double newValue, IEnumerable<Unit> newUnits)
        {
            if (oldUnit is not null)
                _units.Remove(oldUnit);
            if (newUnits is not null)
                _units.AddRange(newUnits);
            Value = newValue;
            return this;
        }
        internal ValueUnit ReplaceUnits(IEnumerable<Unit> oldUnits, double newValue, Unit newUnit)
        {
            if (oldUnits is not null)
            {
                foreach (var oldUnit in oldUnits)
                    _units.Remove(oldUnit);
            }
            if (newUnit is not null)
                _units.Add(newUnit);
            Value = newValue;
            return this;
        }

        public bool CompareUnits(ValueUnit valueUnit)
        {
            if (Enumerable.SequenceEqual(this.Units.OrderBy(u => u), valueUnit.Units.OrderBy(u => u)))
                return true;

            this.OrganizeUnits();
            valueUnit.OrganizeUnits();

            if (Enumerable.SequenceEqual(this.Units.OrderBy(u => u), valueUnit.Units.OrderBy(u => u)))
                return true;

            var unitContainer = new UnitContainerComparator(Units);
            if (unitContainer.Compare(valueUnit.Units))
                return true;

            return false;
        }

        public int CompareTo([AllowNull] ValueUnit other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException(nameof(other));

            if (!CompareUnits(other))
                throw new ArgumentException("Units not matched");

            if (Value > other.Value)
                return 1;
            if (Value < other.Value)
                return -1;
            return 0;
        }

        public int CompareTo(object obj)
        {
            if (obj is ValueUnit valueUnit)
                return CompareTo(valueUnit);

            throw new ArgumentException("Object is not a value unit.");
        }

        public override string ToString()
        {
            return ToString(CultureInfo.DefaultThreadCurrentCulture);
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach (var unit in Units)
            {
                hashCode += unit.Power.GetHashCode() * 3;
                hashCode += unit.Symbol.GetHashCode() * 5;
            }

            return Value.GetHashCode() * 7 + hashCode;
        }

        public bool Equals(ValueUnit other)
        {
            if (other is null)
                return false;
            if (this.CompareUnits(other))
                return other.Value.Equals(this.Value);
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is ValueUnit valueUnit)
            {
                if (this.CompareUnits(valueUnit))
                    return valueUnit.Value.Equals(this.Value);
                return false;
            }
            if (this.Units.Count() == 0)
                return obj.Equals(this.Value);

            OrganizeUnits();
            if (this.Units.Count() == 0)
                return obj.Equals(this.Value);

            return false;
        }

        #region UnitOperations

        public static ValueUnit operator +(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                return null;

            if (leftValue.CompareUnits(rightValue))
                return new ValueUnit(leftValue.Value + rightValue.Value, leftValue.Units.ToArray());

            throw new ArgumentException("Wrong parameters. Don't match.");
        }

        public static ValueUnit operator -(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                return null;

            if (leftValue.CompareUnits(rightValue))
                return new ValueUnit(leftValue.Value - rightValue.Value, leftValue.Units.ToArray());

            throw new ArgumentException("Wrong parameters. Don't match.");
        }

        public static ValueUnit operator *(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                return null;

            return new ValueUnit(leftValue.Value * rightValue.Value,
                leftValue.Units.Concat(rightValue.Units).ToArray());
        }

        public static ValueUnit operator /(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                return null;

            return new ValueUnit(leftValue.Value / rightValue.Value,
                leftValue.Units.Concat(rightValue.Units.Select(u => u.Copy(u.Power * -1))).ToArray());
        }

        public static bool operator ==(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) && !ReferenceEquals(rightValue, null) ||
                !ReferenceEquals(leftValue, null) && ReferenceEquals(rightValue, null))
                return false;

            if (ReferenceEquals(leftValue, rightValue))
                return true;

            if (leftValue.CompareUnits(rightValue))
                return leftValue.Equals(rightValue);
            return false;
        }

        public static bool operator !=(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) && !ReferenceEquals(rightValue, null) ||
                !ReferenceEquals(leftValue, null) && ReferenceEquals(rightValue, null))
                return true;

            if (ReferenceEquals(leftValue, rightValue))
                return false;

            if (leftValue.CompareUnits(rightValue))
                return !leftValue.Equals(rightValue);
            return true;
        }

        public static bool operator >(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                throw new ArgumentNullException();

            return leftValue.CompareTo(rightValue) > 0;
        }

        public static bool operator <(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                throw new ArgumentNullException();

            return leftValue.CompareTo(rightValue) < 0;
        }

        public static bool operator >=(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                throw new ArgumentNullException();

            return leftValue.CompareTo(rightValue) >= 0;
        }

        public static bool operator <=(ValueUnit leftValue, ValueUnit rightValue)
        {
            if (ReferenceEquals(leftValue, null) || ReferenceEquals(rightValue, null))
                throw new ArgumentNullException();

            return leftValue.CompareTo(rightValue) <= 0;
        }

        #endregion // UnitOperations

        #region ByteOperators

        public static ValueUnit operator +(Byte a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, Byte b) => a + new ValueUnit(b);
        public static ValueUnit operator -(Byte a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, Byte b) => a - new ValueUnit(b);
        public static ValueUnit operator *(Byte a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Byte b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(Byte a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Byte b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, Byte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(Byte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, Byte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(Byte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, Byte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(Byte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, Byte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(Byte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, Byte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(Byte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, Byte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(Byte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // ByteOperators

        #region SByteOperators

        public static ValueUnit operator +(SByte a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, SByte b) => a + new ValueUnit(b);
        public static ValueUnit operator -(SByte a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, SByte b) => a - new ValueUnit(b);
        public static ValueUnit operator *(SByte a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, SByte b) => new ValueUnit(b * a.Value, a.Units.ToArray());
        public static ValueUnit operator /(SByte a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, SByte b) => new ValueUnit(b / a.Value, a.Units.ToArray());
        public static bool operator <(ValueUnit a, SByte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(SByte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, SByte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(SByte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, SByte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(SByte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, SByte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(SByte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, SByte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(SByte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, SByte b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(SByte a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // SByteOperators

        #region Int16Operators

        public static ValueUnit operator +(Int16 a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, Int16 b) => a + new ValueUnit(b);
        public static ValueUnit operator -(Int16 a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, Int16 b) => a - new ValueUnit(b);
        public static ValueUnit operator *(Int16 a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Int16 b) => new ValueUnit(b * a.Value, a.Units.ToArray());
        public static ValueUnit operator /(Int16 a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Int16 b) => new ValueUnit(b / a.Value, a.Units.ToArray());
        public static bool operator <(ValueUnit a, Int16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(Int16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, Int16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(Int16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, Int16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(Int16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, Int16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(Int16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, Int16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(Int16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, Int16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(Int16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // Int16Operators

        #region UInt16Operators

        public static ValueUnit operator +(UInt16 a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, UInt16 b) => a + new ValueUnit(b);
        public static ValueUnit operator -(UInt16 a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, UInt16 b) => a - new ValueUnit(b);
        public static ValueUnit operator *(UInt16 a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, UInt16 b) => new ValueUnit(b * a.Value, a.Units.ToArray());
        public static ValueUnit operator /(UInt16 a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, UInt16 b) => new ValueUnit(b / a.Value, a.Units.ToArray());
        public static bool operator <(ValueUnit a, UInt16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(UInt16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, UInt16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(UInt16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, UInt16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(UInt16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, UInt16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(UInt16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, UInt16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(UInt16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, UInt16 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(UInt16 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // UInt16Operators

        #region Int32Operators

        public static ValueUnit operator +(Int32 a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, Int32 b) => a + new ValueUnit(b);
        public static ValueUnit operator -(Int32 a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, Int32 b) => a - new ValueUnit(b);
        public static ValueUnit operator *(Int32 a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Int32 b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(Int32 a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Int32 b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, Int32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(Int32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, Int32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(Int32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, Int32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(Int32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, Int32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(Int32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, Int32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(Int32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, Int32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(Int32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // Int32Operators

        #region UInt32Operators

        public static ValueUnit operator +(UInt32 a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, UInt32 b) => a + new ValueUnit(b);
        public static ValueUnit operator -(UInt32 a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, UInt32 b) => a - new ValueUnit(b);
        public static ValueUnit operator *(UInt32 a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, UInt32 b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(UInt32 a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, UInt32 b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, UInt32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(UInt32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, UInt32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(UInt32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, UInt32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(UInt32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, UInt32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(UInt32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, UInt32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(UInt32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, UInt32 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(UInt32 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // UInt32Operators

        #region Int64Operators

        public static ValueUnit operator +(Int64 a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, Int64 b) => a + new ValueUnit(b);
        public static ValueUnit operator -(Int64 a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, Int64 b) => a - new ValueUnit(b);
        public static ValueUnit operator *(Int64 a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Int64 b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(Int64 a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Int64 b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, Int64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(Int64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, Int64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(Int64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, Int64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(Int64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, Int64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(Int64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, Int64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(Int64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, Int64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(Int64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // Int64Operators

        #region UInt64Operators

        public static ValueUnit operator +(UInt64 a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, UInt64 b) => a + new ValueUnit(b);
        public static ValueUnit operator -(UInt64 a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, UInt64 b) => a - new ValueUnit(b);
        public static ValueUnit operator *(UInt64 a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, UInt64 b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(UInt64 a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, UInt64 b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, UInt64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(UInt64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, UInt64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(UInt64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, UInt64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(UInt64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, UInt64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(UInt64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, UInt64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(UInt64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, UInt64 b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(UInt64 a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // UInt64Operators

        #region SingleOperators

        public static ValueUnit operator +(Single a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, Single b) => a + new ValueUnit(b);
        public static ValueUnit operator -(Single a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, Single b) => a - new ValueUnit(b);
        public static ValueUnit operator *(Single a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Single b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(Single a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Single b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, Single b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(Single a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, Single b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(Single a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, Single b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(Single a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, Single b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(Single a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, Single b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(Single a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, Single b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(Single a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // SingleOperators

        #region DoubleOperators

        public static ValueUnit operator +(Double a, ValueUnit b) => new ValueUnit(a) + b;
        public static ValueUnit operator +(ValueUnit a, Double b) => a + new ValueUnit(b);
        public static ValueUnit operator -(Double a, ValueUnit b) => new ValueUnit(a) - b;
        public static ValueUnit operator -(ValueUnit a, Double b) => a - new ValueUnit(b);
        public static ValueUnit operator *(Double a, ValueUnit b) => new ValueUnit(a * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Double b) => new ValueUnit(a.Value * b, a.Units.ToArray());
        public static ValueUnit operator /(Double a, ValueUnit b) => new ValueUnit(a / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Double b) => new ValueUnit(a.Value / b, a.Units.ToArray());
        public static bool operator <(ValueUnit a, Double b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < b;
        public static bool operator <(Double a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a < b.Value;
        public static bool operator >(ValueUnit a, Double b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > b;
        public static bool operator >(Double a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a > b.Value;
        public static bool operator <=(ValueUnit a, Double b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= b;
        public static bool operator <=(Double a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a <= b.Value;
        public static bool operator >=(ValueUnit a, Double b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= b;
        public static bool operator >=(Double a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a >= b.Value;
        public static bool operator ==(ValueUnit a, Double b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == b;
        public static bool operator ==(Double a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a == b.Value;
        public static bool operator !=(ValueUnit a, Double b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != b;
        public static bool operator !=(Double a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a != b.Value;

        #endregion // DoubleOperators

        #region DecimalOperators

        public static ValueUnit operator +(Decimal a, ValueUnit b) => new ValueUnit(Convert.ToDouble(a)) + b;
        public static ValueUnit operator +(ValueUnit a, Decimal b) => a + new ValueUnit(Convert.ToDouble(b));
        public static ValueUnit operator -(Decimal a, ValueUnit b) => new ValueUnit(Convert.ToDouble(a)) - b;
        public static ValueUnit operator -(ValueUnit a, Decimal b) => a - new ValueUnit(Convert.ToDouble(b));
        public static ValueUnit operator *(Decimal a, ValueUnit b) => new ValueUnit(Convert.ToDouble(a) * b.Value, b.Units.ToArray());
        public static ValueUnit operator *(ValueUnit a, Decimal b) => new ValueUnit(a.Value * Convert.ToDouble(b), a.Units.ToArray());
        public static ValueUnit operator /(Decimal a, ValueUnit b) => new ValueUnit(Convert.ToDouble(a) / b.Value, b.Units.ToArray());
        public static ValueUnit operator /(ValueUnit a, Decimal b) => new ValueUnit(a.Value / Convert.ToDouble(b), a.Units.ToArray());
        public static bool operator <(ValueUnit a, Decimal b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value < Convert.ToDouble(b);
        public static bool operator <(Decimal a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : Convert.ToDouble(a) < b.Value;
        public static bool operator >(ValueUnit a, Decimal b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value > Convert.ToDouble(b);
        public static bool operator >(Decimal a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : Convert.ToDouble(a) > b.Value;
        public static bool operator <=(ValueUnit a, Decimal b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value <= Convert.ToDouble(b);
        public static bool operator <=(Decimal a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : Convert.ToDouble(a) <= b.Value;
        public static bool operator >=(ValueUnit a, Decimal b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value >= Convert.ToDouble(b);
        public static bool operator >=(Decimal a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : Convert.ToDouble(a) >= b.Value;
        public static bool operator ==(ValueUnit a, Decimal b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value == Convert.ToDouble(b);
        public static bool operator ==(Decimal a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : Convert.ToDouble(a) == b.Value;
        public static bool operator !=(ValueUnit a, Decimal b) => a.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : a.Value != Convert.ToDouble(b);
        public static bool operator !=(Decimal a, ValueUnit b) => b.HasUnits ? throw new InvalidOperationException("Couldn't compare.") : Convert.ToDouble(a) != b.Value;

        #endregion // DecimalOperators

        #region Conversions

        public static explicit operator ValueUnit(Byte a) => new ValueUnit(a);
        public static explicit operator Byte(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (Byte)a.Value;

        public static explicit operator ValueUnit(SByte a) => new ValueUnit(a);
        public static explicit operator SByte(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (SByte)a.Value;

        public static explicit operator ValueUnit(Int16 a) => new ValueUnit(a);
        public static explicit operator Int16(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (Int16)a.Value;

        public static explicit operator ValueUnit(UInt16 a) => new ValueUnit(a);
        public static explicit operator UInt16(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (UInt16)a.Value;

        public static explicit operator ValueUnit(Int32 a) => new ValueUnit(a);
        public static explicit operator Int32(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (Int32)a.Value;

        public static explicit operator ValueUnit(UInt32 a) => new ValueUnit(a);
        public static explicit operator UInt32(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (UInt32)a.Value;

        public static explicit operator ValueUnit(Int64 a) => new ValueUnit(a);
        public static explicit operator Int64(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (Int64)a.Value;

        public static explicit operator ValueUnit(UInt64 a) => new ValueUnit(a);
        public static explicit operator UInt64(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (UInt64)a.Value;

        public static explicit operator ValueUnit(Single a) => new ValueUnit(a);
        public static explicit operator Single(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (Single)a.Value;

        public static explicit operator ValueUnit(Double a) => new ValueUnit(a);
        public static explicit operator Double(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : a.Value;

        public static explicit operator ValueUnit(Decimal a) => new ValueUnit(Convert.ToDouble(a));
        public static explicit operator Decimal(ValueUnit a) => a.HasUnits ? throw new InvalidOperationException("Couldn't convert.") : (Decimal)a.Value;

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            if (provider is UnitFormatProvider unitFormatProvider && unitFormatProvider.OrganizeUnits)
                this.OrganizeUnits();
            return Convert.ToDecimal(this.Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            if (provider is UnitFormatProvider unitFormatProvider && unitFormatProvider.OrganizeUnits)
                this.OrganizeUnits();
            return this.Value;
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            var groupedUnits = Units.GroupBy(u => u.Symbol);
            List<Unit> formattedUnits = new List<Unit>();
            foreach (var unitGroup in groupedUnits)
            {
                if (UnitRegistry.Instance.RegisteredUnits.ContainsKey(unitGroup.Key))
                    formattedUnits.Add(UnitRegistry.Instance.RegisteredUnits[unitGroup.Key](unitGroup.Sum(ug => ug.Power)));
                else
                    formattedUnits.Add(new CustomUnit(unitGroup.Key, unitGroup.Sum(ug => ug.Power)));
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < formattedUnits.Count; i++)
            {
                var unit = formattedUnits[i];
                if (unit.Power != 0)
                {
                    result.Append(unit.Symbol);
                    if (Math.Abs(unit.Power) != 1 || i == 0 && unit.Power == -1)
                        result.Append("^").Append(i == 0 ? unit.Power : Math.Abs(unit.Power));
                }
                if (i + 1 < formattedUnits.Count)
                {
                    if (unit.Power == 0)
                        continue;

                    if (formattedUnits[i + 1].Power > 0)
                        result.Append("*");
                    else if (formattedUnits[i + 1].Power < 0)
                        result.Append("/");
                }
            }
            return Value.ToString(provider) + result.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(double))
                return ToDouble(provider);
            if (conversionType == typeof(decimal))
                return ToDecimal(provider);
            if (conversionType == typeof(ValueUnit))
                return this;

            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion // Conversions

        public static bool TryParse(object value, out ValueUnit result)
        {
            result =
                value switch
                {
                    ValueUnit => (ValueUnit)value,
                    Byte => (ValueUnit)(Byte)value,
                    SByte => (ValueUnit)(SByte)value,
                    Int16 => (ValueUnit)(Int16)value,
                    UInt16 => (ValueUnit)(UInt16)value,
                    Int32 => (ValueUnit)(Int32)value,
                    UInt32 => (ValueUnit)(UInt32)value,
                    Int64 => (ValueUnit)(Int64)value,
                    UInt64 => (ValueUnit)(UInt64)value,
                    Single => (ValueUnit)(Single)value,
                    Double => (ValueUnit)(Double)value,
                    Decimal => (ValueUnit)(Decimal)value,
                    null => null,
                    _ => null
                };
            return result is not null;
        }
    }
}
