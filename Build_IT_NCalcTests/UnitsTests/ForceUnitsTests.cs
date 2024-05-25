using Build_IT_NCalc.Units;
using Build_IT_NCalc.Units.ForceUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.UnitsTests
{
    public class ForceUnitsTests
    {
        [Fact]
        public void ConversionTest_FromMainUnit()
        {
            var force = new ValueUnit(55, new KiloNewton());
            force.TransformTo<MegaNewton>(force.Units.Last());

            Assert.Equal(0.055, force.Value);
            Assert.Equal("MN", force.Units.Last().Symbol);
        }

        [Fact]
        public void ConversionTest_ChangingUnits()
        {
            var force = new ValueUnit(55000000, new Newton());
            force.Transform<Newton,GigaNewton>();

            Assert.Equal(0.055, force.Value, 3);
            Assert.Equal("GN", force.Units.Last().Symbol);
        }

        [Fact]
        public void ConversionTest_ChangingUnitsWithPower()
        {
            var force = new ValueUnit(2, new KiloNewton(2));
            force.Transform<KiloNewton, Newton>();

            Assert.Equal(2000000, force.Value, 3);
            Assert.Equal("N", force.Units.Last().Symbol);
        }

        [Fact]
        public void ConversionTest_ChangingUnitsWithMinusPower()
        {
            var force = new ValueUnit(2, new MegaNewton(-1));
            force.Transform<MegaNewton, Newton>();

            Assert.Equal(0.000002, force.Value, 5);
            Assert.Equal("N", force.Units.Last().Symbol);
        }
        
        [Fact]
        public void EquationTest_Addition()
        {
            var force1 = new ValueUnit(2, new KiloNewton());
            var force2 = new ValueUnit(20, new Newton());

            var result = force1 + force2;

            Assert.Equal(2.020, result.Value, 3);
            Assert.Equal("kN", result.Units.Last().Symbol);
        }

        [Fact]
        public void EquationTest_Subtraction()
        {
            var force1 = new ValueUnit(2, new KiloNewton());
            var force2 = new ValueUnit(20,new Newton());

            var result = force1 - force2;

            Assert.Equal(1.980, result.Value, 3);
            Assert.Equal("kN", result.Units.Last().Symbol);
        }

        [Fact]
        public void EquationTest_Multiplication()
        {
            var force1 = new ValueUnit(2, new KiloNewton());
            var force2 = new ValueUnit(20, new Newton());

            var result = force1 * force2;

            Assert.Equal(40, result.Value, 3);
            Assert.Equal(2, result.Units.Count());
            Assert.Contains(new KiloNewton(), result.Units);
            Assert.Contains(new Newton(), result.Units);
        }

        [Fact]
        public void EquationTest_MultiplicationWithCleanUp()
        {
            var force1 = new ValueUnit(2, new KiloNewton());
            var force2 = new ValueUnit(20, new Newton());

            var result = force1 * force2;

            result.OrganizeUnits();

            Assert.Equal(0.04, result.Value, 3);
            Assert.Single(result.Units);
            Assert.Contains(new KiloNewton(2), result.Units);
        }

        [Fact]
        public void EquationTest_Division()
        {
            var force1 = new ValueUnit(2, new KiloNewton());
            var force2 = new ValueUnit(20, new Newton());

            var result = force1 / force2;

            Assert.Equal(0.1, result.Value, 3);
            Assert.Equal(2, result.Units.Count());
            Assert.Contains(new KiloNewton(), result.Units);
            Assert.Contains(new Newton(-1), result.Units);
        }

        [Fact]
        public void EquationTest_DivisionWithCleanUp()
        {
            var force1 = new ValueUnit(2, new KiloNewton());
            var force2 = new ValueUnit(20, new Newton());

            var result = force1 / force2;

            result.OrganizeUnits();

            Assert.Equal(100, result.Value, 3);
            Assert.Empty(result.Units);
        }
    }
}
