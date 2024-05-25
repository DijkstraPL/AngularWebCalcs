using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Scripts;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Scripts
{
    [TestFixture]
    public class ScriptMeanCompressiveStrengthAt28Days
    {

        [Test]
        public void ScriptMeanCompressiveStrengthAt28DaysTest_Success()
        {
                var f_ck_ = new ValueUnit(30,  new CustomUnit("MPa", 1));

                var parameterf_ck_ = new InputParameter<ValueUnit>(1, "f_ck_", f_ck_);
                var parameterf_cm_ = new Parameter<string>(2, "f_cm_", "[f_ck_]+Unit(8,'MPa')");

                var script = new Script("ScriptMeanCompressiveStrengthAt28Days",
                    parameterf_ck_, parameterf_cm_);

                var calculatedParameters = script.CalculateScript();

                var calculatedParameterf_cm_ = calculatedParameters.First(p => p.Name == "f_cm_");

                var f_cm_Result = (ValueUnit)calculatedParameterf_cm_.CalculatedValue;
                f_cm_Result.OrganizeUnits();

                Assert.That(calculatedParameterf_cm_.CalculatedValue, Is.EqualTo(new ValueUnit(38,  new CustomUnit("MPa"))));
        }

        //[Test]
        //public void NewlyCreatedScriptTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create(name: "Mean compresive strength of concrete at 28 days",
        //        description: "Calculate mean compressive strength of concrete at 28 days. Base on [PN-EN-1992-1-1:2002 Table 3.1].",
        //        "Eurocode 1992", "Concrete", "Materials", "Strength", "Compressive");

        //    scriptBuilder.SetDocument("PN-EN-1992-1-1:2002")
        //        .SetAuthor("Konrad Kania")
        //        .SetGroupName("Eurocode 2")
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 1,
        //            Name = "f_ck_",
        //            VisibilityValidator = "[f_ck_]>0",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "MPa"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 2,
        //            Name = "f_cm_",
        //            VisibilityValidator = "[f_cm_]>0",
        //            Value = "[f_ck_]+8",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //            Unit = "MPa"
        //        });

        //    var script = scriptBuilder.Build();

        //    var calculationEngine = new CalculationEngine(script.Parameters);

        //    calculationEngine.CalculateFromText("[f_ck_]=30");

        //    Assert.That(38, Is.EqualTo(script.GetParameterByName("f_cm_").Value));
        //}
    }
}
