using Build_IT_NCalc.Units.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_NCalc.Units
{
    internal class UnitContainerComparator
    {
        private readonly IEnumerable<Unit> _units;

        internal UnitContainerComparator(IEnumerable<Unit> units)
        {
            _units = units ?? throw new System.ArgumentNullException(nameof(units));
        }

        internal bool Compare(IEnumerable<Unit> unitsToCompare)
        {
            var decomposedUnits = DecomposeAll(_units);
            var decomposedUnitsToCompare = DecomposeAll(unitsToCompare);

            if (Enumerable.SequenceEqual(decomposedUnits.OrderBy(u => u), decomposedUnitsToCompare.OrderBy(u => u)))
                return true;

            var organizedUnits = OrganizeUnits(decomposedUnits);
            var organizedUnitsToCompare = OrganizeUnits(decomposedUnitsToCompare);

            if (Enumerable.SequenceEqual(organizedUnits.OrderBy(u => u), organizedUnitsToCompare.OrderBy(u => u)))
                return true;

            return false;
        }

        private IEnumerable<Unit> DecomposeAll(IEnumerable<Unit> units)
        {
            while (units.Any(u => u.CanDecompose()))
            {
                units = DecomposeOnce(units);
            }
            return units;
        }

        private IEnumerable<Unit> DecomposeOnce(IEnumerable<Unit> units)
        {
            var decomposedUnits = new List<Unit>();
            foreach (var unit in units)
            {
                decomposedUnits.AddRange(unit.Decompose());
            }
            return decomposedUnits;
        }

        private IEnumerable<Unit> OrganizeUnits(IEnumerable<Unit> units)
        {
            var groupedUnits = units.GroupBy(u => u.Symbol);
            var newUnits = new List<Unit>();
            foreach (var group in groupedUnits)
            {
                var newUnit = group.First();
                newUnit.Power = group.Sum(g => g.Power);
                if (newUnit.Power != 0)
                    newUnits.Add(newUnit);
            }
            return newUnits;
        }
    }
}
