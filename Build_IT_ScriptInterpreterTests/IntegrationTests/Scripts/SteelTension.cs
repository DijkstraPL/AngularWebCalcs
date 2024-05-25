using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Scripts;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Scripts
{
    [TestFixture]
    public class SteelTension
    {
        [Test]
        public void SteelTensionTest_Success()
        {
                var A = new ValueUnit(60,  new CustomUnit("cm", 2));
                var f_y_ = new ValueUnit(235, new CustomUnit("MPa", 1));
                var N_Ed_ = new ValueUnit(1400,  new CustomUnit("kN", 1));
                double gamma_M0_ = 1.0;

                var parameterA = new InputParameter<ValueUnit>(1, "A", A);
                var parameterf_y_ = new InputParameter<ValueUnit>(2, "f_y_", f_y_);
                var parameterN_Ed_ = new InputParameter<ValueUnit>(3, "N_Ed_", N_Ed_);
                var parametergamma_M0_ = new InputParameter<double>(4, "γ_M0_", gamma_M0_);
                var parameterNplRd = new Parameter<string>(5, "N_pl,Rd_", "[A]*[f_y_]/[γ_M0_]");
                var parameterResistance = new Parameter<string>(6, "Resistance", "[N_Ed_]/[N_pl,Rd_]");

                var script = new Script("SteelTension",
                    parameterA, parameterf_y_, parameterN_Ed_, parametergamma_M0_,
                    parameterNplRd, parameterResistance);

                var calculatedParameters = script.CalculateScript();

                var calculatedParameterNplRd = calculatedParameters.First(p => p.Name == "N_pl,Rd_");
                var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");
                var resistanceResult = (ValueUnit)calculatedParameterResistance.CalculatedValue;
                resistanceResult.OrganizeUnits();

                Assert.Multiple(() =>
                {
                    Assert.That(calculatedParameterNplRd.CalculatedValue, Is.EqualTo(new ValueUnit(1410, new CustomUnit("kN"))));
                    Assert.AreEqual(0.9929078014184397, resistanceResult.Value, 0.001);
                    CollectionAssert.IsEmpty(resistanceResult.Units);
                });
        }



        [Test]
        public void SteelTensionTest_ShouldReuseSameScriptObject_Success()
        {
                double gamma_M0_ = 1.0;
                var parameterA = new InputParameter<ValueUnit>(1, "A", null);
                var parameterf_y_ = new InputParameter<ValueUnit>(2, "f_y_", null);
                var parameterN_Ed_ = new InputParameter<ValueUnit>(3, "N_Ed_", null);
                var parametergamma_M0_ = new InputParameter<double>(4, "γ_M0_", gamma_M0_);
                var parameterNplRd = new Parameter<string>(5, "N_pl,Rd_", "[A]*[f_y_]/[γ_M0_]");
                var parameterResistance = new Parameter<string>(6, "Resistance", "[N_Ed_]/[N_pl,Rd_]");

                var script = new Script("SteelTension",
                    parameterA, parameterf_y_, parameterN_Ed_, parametergamma_M0_,
                    parameterNplRd, parameterResistance);

                var A = new ValueUnit(60, new CustomUnit("cm", 2));
                var f_y_ = new ValueUnit(235, new CustomUnit("MPa", 1));
                var N_Ed_ = new ValueUnit(1400, new CustomUnit("kN", 1));

                ValueUnit resistanceResult = Calculate(script, A, f_y_, N_Ed_);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(0.9929078014184397, resistanceResult.Value, 0.001);
                    CollectionAssert.IsEmpty(resistanceResult.Units);
                });

                A = new ValueUnit(60,  new CustomUnit("cm", 2));
                f_y_ = new ValueUnit(355,  new CustomUnit("MPa", 1));
                N_Ed_ = new ValueUnit(3600,  new CustomUnit("kN", 1));

                ValueUnit resistanceResult2 = Calculate(script, A, f_y_, N_Ed_);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1.690141, resistanceResult2.Value, 0.001);
                    CollectionAssert.IsEmpty(resistanceResult2.Units);
                });
        }

        private static ValueUnit Calculate(Script script, ValueUnit A, ValueUnit f_y_, ValueUnit N_Ed_)
        {
            var calculatedParameters = script.CalculateScript(("A", A), ("f_y_", f_y_), ("N_Ed_", N_Ed_));

            var calculatedParameterNplRd = calculatedParameters.First(p => p.Name == "N_pl,Rd_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");
            var resistanceResult = (ValueUnit)calculatedParameterResistance.CalculatedValue;
            resistanceResult.OrganizeUnits();
            return resistanceResult;
        }



        //[Test]
        //public void CreationTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create(name: "Steel tension",
        //        description: "Calculate tension resistance. Base on [PN-EN-1993-1-1:2005 6.2.3.(2)a)].",
        //        "Eurocode 1993", "Steel", "Tension", "Resistance");

        //    scriptBuilder.SetAuthor("Konrad Kania");
        //    scriptBuilder.SetDocument("PN-EN-1993-1-1:2005");
        //    scriptBuilder.SetGroupName("Eurocode 3");
        //    scriptBuilder.SetNotes("Net area not included.");

        //    scriptBuilder
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 1,
        //            Name = "N_Ed_",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "kN"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 2,
        //            Name = "A",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "cm^2^"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 3,
        //            Name = "f_y_",
        //            ValueType = ValueTypes.Number,
        //            ValueOptions = new List<ValueOption>()
        //            {
        //                new ValueOption(235),
        //                new ValueOption(275),
        //                new ValueOption(355),
        //                new ValueOption(420),
        //                new ValueOption(440),
        //                new ValueOption(460),
        //                new ValueOption(null),
        //            },
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "MPa"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 4,
        //            Name = "γ_M0_",
        //            Value = 1.0,
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.StaticData,
        //            Unit = ""
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 5,
        //            Name = "N_pl,Rd_",
        //            Value = "[A]*[f_y_]/[γ_M0_]/10",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //            Unit = "kN"
        //        });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 27,
        //        Name = "Resistance",
        //        Value = "[N_Ed_]/[N_pl,Rd_]*100",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "%"
        //    });

        //    var script = scriptBuilder.Build();

        //    var calculationEngine = new CalculationEngine(script.Parameters);
        //    calculationEngine.CalculateFromText("[A]=60|[f_y_]=235|[N_Ed_]=1400");

        //    Assert.That(script.GetParameterByName("N_pl,Rd_").Value, Is.EqualTo(1410).Within(0.000001));
        //    Assert.That(script.GetParameterByName("Resistance").Value, Is.EqualTo(99.29078).Within(0.000001));
        //}
    }
}
