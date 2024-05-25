using NUnit.Framework;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Scripts
{
    [TestFixture]
    public class ScriptBuilderTests
    {
        //[Test]
        //public void MomentOfInteriaScriptTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create(name: "Moment of interia for rectangle",
        //        description: "Calculate moment of interia for rectangle",
        //        "Section", "Moment of interia", "Rectangle");

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 1,
        //        Name = "b",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible
        //    })
        //    .AppendParameter(new Parameter()
        //    {
        //        Number = 2,
        //        Name = "h",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 3,
        //        Name = "I",
        //        Value= "[b]*Pow([h],3)/12",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible
        //    });

        //    var script = scriptBuilder.Build();
        //    var calculationEngine = new CalculationEngine(script.Parameters);
        //    calculationEngine.CalculateFromText("[b]=50|[h]=70");

        //    Assert.That(1429166.667, Is.EqualTo(script.GetParameterByName("I").Value).Within(0.001));
        //}

        //[Test]
        //public void MomentOfInteriaScriptTest_CalculateAsAList()
        //{
        //    var scriptBuilder = ScriptBuilder.Create(name: "Moment of interia for rectangle",
        //        description: "Calculate moment of interia for rectangle",
        //        "Section", "Moment of interia", "Rectangle");

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 1,
        //        Name = "b",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible
        //    })
        //    .AppendParameter(new Parameter()
        //    {
        //        Number = 2,
        //        Name = "h",
        //        ValueType = ValueTypes.List,
        //        ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 3,
        //        Name = "I",
        //        Value = "[b]*Pow([h],3)/12",
        //        ValueType = ValueTypes.List,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible
        //    });

        //    var script = scriptBuilder.Build();
        //    var calculationEngine = new CalculationEngine(script.Parameters);
        //    calculationEngine.CalculateFromText("[b]=50|[h]=70;1");

        //    var result = script.GetParameterByName("I").Value as List<object>;

        //    Assert.That(2, Is.EqualTo(result.Count));
        //    Assert.That(1429166.667, Is.EqualTo(result[0]).Within(0.001));
        //    Assert.That(4.167, Is.EqualTo(result[1]).Within(0.001));
        //}
    }
}
