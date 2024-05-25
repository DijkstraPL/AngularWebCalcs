using Build_IT_NCalc.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Build_IT_NCalcTests.UnitsTests
{
    public class ValueUnitTests
    {
        [Fact]
        public void ConvertBoxedDoubleToValueUnit()
        {
            double initValue = 1.2;
            object initValueObj = initValue;
             ValueUnit.TryParse(initValueObj, out ValueUnit result);
            Assert.Equal(new ValueUnit(1.2), result);
        }

        [Fact]
        public void EqualityTest()
        {
            ValueUnit a = new ValueUnit(1, "mm");
            ValueUnit b = new ValueUnit(1, "m");
            Assert.False(a == b);
        }
        [Fact]
        public void NonEqualityTest()
        {
            ValueUnit a = new ValueUnit(1000, "mm");
            ValueUnit b = new ValueUnit(1, "m");
            Assert.False(a != b);
        }
        [Fact]
        public void CompareUnitsTest()
        {
            ValueUnit a = new ValueUnit(1, "mm");
            ValueUnit b = new ValueUnit(1, "m");
            Assert.True(a.CompareUnits(b));
        }

        [Fact]
        public void CompareUnitsTest_Complex()
        {
            ValueUnit a = new ValueUnit(1, new CustomUnit("kN"), new CustomUnit("m"), new CustomUnit("kPa", -1), new CustomUnit("m", -3));
            ValueUnit b = new ValueUnit(1);
            Assert.True(a.CompareUnits(b));
        }

        [Fact]
        public void UnitsDecomposition()
        {
            ValueUnit a = new ValueUnit(1, new CustomUnit("kN"), new CustomUnit("m"));
            ValueUnit b = new ValueUnit(1, new CustomUnit("kPa"), new CustomUnit("m", 3));
           
            Assert.True(a.CompareUnits(b));
        }
        [Fact]
        public void UnitsDecomposition2()
        {
            ValueUnit a = new ValueUnit(1, new CustomUnit("kN"));
            ValueUnit b = new ValueUnit(1, new CustomUnit("kg"), new CustomUnit("m"), new CustomUnit("s", -2));

            Assert.True(a.CompareUnits(b));
        }
        [Fact]
        public void UnitsDecomposition3()
        {
            ValueUnit a = new ValueUnit(1, new CustomUnit("kN"));
            ValueUnit b = new ValueUnit(1, new CustomUnit("kg"), new CustomUnit("m"), new CustomUnit("min", -2));

            Assert.True(a.CompareUnits(b));
        }
        // TODO: Fix this one!
        //[Fact]
        //public void UnitsDecomposition4()
        //{
        //    ValueUnit a = new ValueUnit(1, new CustomUnit("kN",2));
        //    ValueUnit b = new ValueUnit(1, new CustomUnit("kg",2), new CustomUnit("m", 2), new CustomUnit("s", -4));

        //    Assert.True(a.CompareUnits(b));
        //}

        [Fact]
        public void ValueComparision()
        {
            ValueUnit a = new ValueUnit(1d/3600, new CustomUnit("N"));
            ValueUnit b = new ValueUnit(1, new CustomUnit("kg"), new CustomUnit("m"), new CustomUnit("min", -2));

            Assert.True(a.Equals(b));
        }
        [Fact]
        public void ValueComparision2()
        {
            ValueUnit a = new ValueUnit(1, new CustomUnit("N"));
            ValueUnit b = new ValueUnit(1, new CustomUnit("kg"), new CustomUnit("m"), new CustomUnit("s", -2));

            Assert.True(a.Equals(b));
        }
    }
}
