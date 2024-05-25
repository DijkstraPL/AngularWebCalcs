using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Scripts;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Scripts
{
    [TestFixture]
    public class ScriptCompressiveStrengthAtAge
    {
        [Test]
        public void ScriptCompressiveStrengthAtAgeTest_Success()
        {
            var f_ck_ = new ValueUnit(30, new CustomUnit("MPa", 1));
            var f_cm_ = new ValueUnit(38, new CustomUnit("MPa", 1));
            var t = new ValueUnit(5, new CustomUnit("day", 1));

            var parameterf_ck_ = new InputParameter<ValueUnit>(1, "f_ck_", f_ck_);
            var parameterf_cm_ = new InputParameter<ValueUnit>(2, "f_cm_", f_cm_);
            var parameterCement_type_ = new InputParameter<string>(3, "cement_type_", "CEM 42,5R");
            var parameterT = new InputParameter<ValueUnit>(4, "t", t);
            var parameterS = new Parameter<string>(5, "s", "if(in([cement_type_],'CEM 42,5R','CEM 52,5N', 'CEM 52,5R') == true,0.2," +
                    "if(in([cement_type_],'CEM 32,5R','CEM 42,5') == true,0.25," +
                    "if(in([cement_type_],'CEM 32,5N') == true,0.38, ERROR('Invalid cement type.'))))");
            var parameterβ_cc_t = new Parameter<string>(6, "β_cc_(t)", "Exp([s]*(1-Sqrt(Unit(28,'day')/[t])))");
            var parameterf_cm_t = new Parameter<string>(7, "f_cm_(t)", "[β_cc_(t)]*[f_cm_]");
            var parameterf_ck_t = new Parameter<string>(8, "f_ck_(t)", "if([t]>=Unit(28,'day'),[f_ck_],if([t]>Unit(3,'day'),[f_cm_(t)]-Unit(8,'MPa'),ERROR('Not even 3 days.')))");

            var script = new Script("ScriptCompressiveStrengthAtAge",
                parameterf_ck_, parameterf_cm_, parameterCement_type_, parameterT,
                parameterS, parameterβ_cc_t, parameterf_cm_t, parameterf_ck_t);

            var calculatedParameters = script.CalculateScript();

            var calculatedParameterS = calculatedParameters.First(p => p.Name == "s");
            var calculatedParameterβ_cc_t = calculatedParameters.First(p => p.Name == "β_cc_(t)");
            var calculatedParameterf_cm_t = calculatedParameters.First(p => p.Name == "f_cm_(t)");
            var calculatedParameterf_ck_t = calculatedParameters.First(p => p.Name == "f_ck_(t)");
            var β_cc_tResult = (ValueUnit)calculatedParameterβ_cc_t.CalculatedValue;
            β_cc_tResult.RoundValue(0.000001);
            var f_cm_tResult = (ValueUnit)calculatedParameterf_cm_t.CalculatedValue;
            f_cm_tResult.OrganizeUnits();
            var f_ck_tResult = (ValueUnit)calculatedParameterf_ck_t.CalculatedValue;
            f_ck_tResult.OrganizeUnits();

            Assert.Multiple(() =>
            {
                Assert.That(calculatedParameterS.CalculatedValue, Is.EqualTo(0.2));
                Assert.That(calculatedParameterβ_cc_t.CalculatedValue, Is.EqualTo(new ValueUnit(0.760875)));
                Assert.That(calculatedParameterf_cm_t.CalculatedValue, Is.EqualTo(new ValueUnit(28.913244492606958, new CustomUnit("MPa"))));
                Assert.That(calculatedParameterf_ck_t.CalculatedValue, Is.EqualTo(new ValueUnit(20.913244492606958, new CustomUnit("MPa"))));
            });
        }
    }
    //[Test]
    //public void NewlyCreatedScriptTest_Success()
    //{
    //    var scriptBuilder = ScriptBuilder.Create(name: "Compressive strength of concrete at an age",
    //        description: "Calculate compressive strength of concrete at an age. Base on [PN-EN-1992-1-1:2002 3.1.2].",
    //        "Eurocode 1992", "Concrete", "Materials", "Strength", "Time", "Compressive");

    //    var cementTypes = new List<ValueOption>
    //    {
    //        new ValueOption(value: "CEM 42,5R"),
    //        new ValueOption(value: "CEM 52,5N"),
    //        new ValueOption(value: "CEM 52,5R"),
    //        new ValueOption(value: "CEM 32,5R"),
    //        new ValueOption(value: "CEM 42,5"),
    //        new ValueOption(value: "CEM 32,5N")
    //    };

    //    scriptBuilder.AppendParameter(new Parameter()
    //    {
    //        Number = 1,
    //        Name = "f_ck_",
    //        VisibilityValidator = "[f_ck_]>0",
    //        ValueType = ValueTypes.Number,
    //        ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
    //        Unit = "MPa"
    //    })
    //        .AppendParameter(new Parameter()
    //        {
    //            Number = 2,
    //            Name = "f_cm_",
    //            VisibilityValidator = "[f_cm_]>0",
    //            ValueType = ValueTypes.Number,
    //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
    //            Unit = "MPa",
    //        })
    //        .AppendParameter(new Parameter()
    //        {
    //            Number = 3,
    //            Name = "cement_type_",
    //            ValueOptions = cementTypes,
    //            ValueType = ValueTypes.Text,
    //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
    //            Unit = "-"
    //        })
    //        .AppendParameter(new Parameter()
    //        {
    //            Number = 4,
    //            Name = "t",
    //            VisibilityValidator = "[t]>3",
    //            ValueType = ValueTypes.Number,
    //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
    //            Unit = "day"
    //        });

    //    scriptBuilder.AppendParameter(new Parameter()
    //    {
    //        Number = 10,
    //        Name = "s",
    //        Value = "if(in([cement_type_],'CEM 42,5R','CEM 52,5N', 'CEM 52,5R') == true,0.2," +
    //        "if(in([cement_type_],'CEM 32,5R','CEM 42,5') == true,0.25," +
    //        "if(in([cement_type_],'CEM 32,5N') == true,0.38, ERROR('Invalid cement type.'))))",
    //        ValueType = ValueTypes.Number,
    //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
    //        Unit = "-"
    //    });

    //    scriptBuilder.AppendParameter(new Parameter()
    //    {
    //        Number = 11,
    //        Name = "β_cc_(t)",
    //        Value = "Exp([s]*(1-Sqrt(28/[t])))",
    //        ValueType = ValueTypes.Number,
    //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
    //        Unit = "-"
    //    });

    //    scriptBuilder.AppendParameter(new Parameter()
    //    {
    //        Number = 12,
    //        Name = "f_cm_(t)",
    //        Value = "[β_cc_(t)]*[f_cm_]",
    //        ValueType = ValueTypes.Number,
    //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
    //        Unit = "MPa"
    //    });

    //    scriptBuilder.AppendParameter(new Parameter()
    //    {
    //        Number = 13,
    //        Name = "f_ck_(t)",
    //        Value = "if([t]>=28,[f_ck_],if([t]>3,[f_cm_(t)]-8,ERROR('Not even 3 days.')))",
    //        ValueType = ValueTypes.Number,
    //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
    //        Unit = "MPa"
    //    });

    //    var script = scriptBuilder.Build();

    //    var calculationEngine = new CalculationEngine(script.Parameters);

    //    calculationEngine.CalculateFromText("[f_ck_]=30|[f_cm_]=38|[cement_type_]=CEM 42,5R|[t]=5");

    //    Assert.That(0.2, Is.EqualTo(script.GetParameterByName("s").Value).Within(0.000001));
    //    Assert.That(0.760874, Is.EqualTo(script.GetParameterByName("β_cc_(t)").Value).Within(0.000001));
    //    Assert.That(28.913244, Is.EqualTo(script.GetParameterByName("f_cm_(t)").Value).Within(0.000001));
    //    Assert.That(20.913244, Is.EqualTo(script.GetParameterByName("f_ck_(t)").Value).Within(0.000001));
    //}
}

