using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIP = Build_IT_ScriptInterpreter.Parameters;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Parameters
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void FormatTest()
        {
            var input = "m*K";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("m", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("K", 1)));
            });
        }

        [Test]
        public void FormatTest2()
        {
            var input = "m/K";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("m", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("K", -1)));
            });
        }

        [Test]
        public void FormatTest3()
        {
            var input = "m/K*kN";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("m", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("K", -1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("kN", 1)));
            });
        }

        [Test]
        public void FormatTest4()
        {
            var input = "kPa^3/K^-2*kN";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("kPa", 3)));
                Assert.That(formattedUnits.Contains(new CustomUnit("K", 2)));
                Assert.That(formattedUnits.Contains(new CustomUnit("kN", 1)));
            });
        }

        [Test]
        public void FormatTest5()
        {
            var input = "(kN*m)/Pa";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("kN", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("m", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("Pa", -1)));
            });
        }

        [Test]
        public void FormatTest6()
        {
            var input = "(kN*m)/(Pa*kg)";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("kN", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("m", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("Pa", -1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("kg", -1)));
            });
        }

        [Test]
        public void FormatTest7()
        {
            var input = "  m * kg ^ 3";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("m", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("kg", 3)));
            });
        }
        [Test]
        public void FormatTest8()
        {
            var input = "(kN/m)/(Pa/kg)";

            var unit = new SIP.Unit(input);
            var formattedUnits = unit.Format();

            Assert.Multiple(() =>
            {
                Assert.That(formattedUnits.Contains(new CustomUnit("kN", 1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("m", -1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("Pa", -1)));
                Assert.That(formattedUnits.Contains(new CustomUnit("kg", 1)));
            });
        }
    }
}
