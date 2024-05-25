using Build_IT_NCalc.Units;
using Build_IT_NCalc.Units.ForceUnits;
using Build_IT_NCalc.Units.LengthUnits;
using Build_IT_NCalc.Units.PressureUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Build_IT_NCalcTests.UnitsTests
{
    public class CompositionUnitsTests
    {
        [Fact]
        public void KiloPascalMultipliedByMeterSquare_ShouldReturnKiloNewton()
        {
            var kiloPascal = new ValueUnit(2,  new KiloPascal());
            var meter = new ValueUnit(3, new Meter(2));

            var result = kiloPascal * meter;
            result.ComposeUnits();

            Assert.Equal(6, result.Value, 3);
            Assert.Contains(new KiloNewton(), result.Units);
        }
    }
}
