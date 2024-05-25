using Build_IT_ScriptInterpreter.CalculationEngine;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Scripts;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Scripts
{
    [TestFixture]
    //TODO: Fix this!
    public class CalculationEngineTests
    {
        //[Test]
        //public void C()
        //{
        //    var ce = new CalculationEngine();

        //    ce.RegisterForCalculation(new CalculationParameter("c", "a+b*2", typeof(double)));
        //    ce.RegisterForCalculation(new CalculationParameter("d", "a*2+b*b+c", typeof(double)));
        //    ce.RegisterForCalculation(new CalculationParameter("e", "d*d", typeof(double)));

        //    var input = new List<InputParameter> { new InputParameter("a", 4), new InputParameter("b", 2.5) };
        //    var result = ce.Calculate(input);
        //    var resultDict = result.ToDictionary(r => r.Name, r => r.Value);
        //    // new List<(string, object)> {  }, //("a", default(int)), ("b", default(double)), ("c", default(double)), ("d", default(double)), ("e", default(int)) 
        //    //     new List<(string, string)> { ("c", "a+b*2"), ("d", "a*2+b*b+c"), ("e", "d*d") });


        //    // ce.Initialize();

        //    //var result = ce.CalculateScript(("a", 4), ("b", 2.5));

        //    Assert.That(resultDict["c"], Is.EqualTo(9));
        //    Assert.That(resultDict["d"], Is.EqualTo(23.25));
        //    Assert.That(resultDict["e"], Is.EqualTo(540.5625));

        //    var input2 = new List<InputParameter> { new InputParameter("a", 1.5), new InputParameter("b", 2) };
        //    var result2 = ce.Calculate(input2);
        //    var resultDict2 = result2.ToDictionary(r => r.Name, r => r.Value);

        //    Assert.That(resultDict2["c"], Is.EqualTo(5.5));
        //    Assert.That(resultDict2["d"], Is.EqualTo(12.5));
        //    Assert.That(resultDict2["e"], Is.EqualTo(156.25));
        //}


        //[Test]
        //public void E()
        //{
        //    var ce = new CalculationEngine3(new List<CalculationInputParameter> {
        //     new CalculationInputParameter("c", "a+b*2", typeof(double)),
        //     new CalculationInputParameter("d", "a*2+b*b+c", typeof(double)),
        //     new CalculationInputParameter("e", "d*d", typeof(double)),
        //    });

        //    ce.Intialize();

        //    var input = new List<InputDataParameter> { new InputDataParameter("a", 4), new InputDataParameter("b", 2.5) };
        //    var result = ce.Calculate(input);

        //    var resultDict = result.ToDictionary(r => r.Name, r => r.Value);
        //    Assert.That(resultDict["c"], Is.EqualTo(9));
        //    Assert.That(resultDict["d"], Is.EqualTo(23.25));
        //    Assert.That(resultDict["e"], Is.EqualTo(540.5625));

        //    var input2 = new List<InputDataParameter> { new InputDataParameter("a", 1.5), new InputDataParameter("b", 2) };
        //    var result2 = ce.Calculate(input2);
        //    var resultDict2 = result2.ToDictionary(r => r.Name, r => r.Value);

        //    Assert.That(resultDict2["c"], Is.EqualTo(5.5));
        //    Assert.That(resultDict2["d"], Is.EqualTo(12.5));
        //    Assert.That(resultDict2["e"], Is.EqualTo(156.25));
        //}

        //[Test]
        //public async Task Cas()
        //{
        //    var ce = new CalculationEngine(
        //        new List<(string, object)> { }, //("a", default(int)), ("b", default(double)), ("c", default(double)), ("d", default(double)), ("e", default(int)) 
        //        new List<(string, string)> { ("c", "a+b*2"), ("d", "a*2+b*b+c"), ("e", "d*d") });

        //    ce.Initialize();

        //    var result = await ce.CalculateScriptAsync(("a", 4), ("b", 2.5));

        //    Assert.That(result["c"], Is.EqualTo(9));
        //    Assert.That(result["d"], Is.EqualTo(23.25));
        //    Assert.That(result["e"], Is.EqualTo(540.5625));

        //    var result2 = await ce.CalculateScriptAsync(("a", 1.5), ("b", 2));

        //    Assert.That(result2["c"], Is.EqualTo(5.5));
        //    Assert.That(result2["d"], Is.EqualTo(12.5));
        //    Assert.That(result2["e"], Is.EqualTo(156.25));
        //}

        //[Test]
        //public void CalculateFromTextTest_Success()
        //{
        //    var parameter1 = new Mock<IParameter>();
        //    parameter1.Setup(p => p.Context).Returns(ParameterOptions.Editable | ParameterOptions.Visible);
        //    parameter1.Setup(p => p.Name).Returns("a");

        //    var parameter2 = new Mock<IParameter>();
        //    parameter2.Setup(p => p.Context).Returns(ParameterOptions.Editable | ParameterOptions.Visible);
        //    parameter2.Setup(p => p.Name).Returns("b");

        //    var parameter3 = new Mock<IParameter>();
        //    parameter3.Setup(p => p.Context).Returns(ParameterOptions.StaticData);
        //    parameter3.Setup(p => p.Name).Returns("c");
        //    parameter3.Setup(p => p.Value).Returns(3);

        //    var parameter4 = new Mock<IParameter>();
        //    parameter4.Setup(p => p.Context).Returns(ParameterOptions.Calculation | ParameterOptions.Visible);
        //    parameter4.Setup(p => p.Name).Returns("d");
        //    parameter4.SetupProperty(p => p.Value);
        //    parameter4.Object.Value = "[a]+[b]*[c]";

        //    var script = new Mock<ICalculatable>();
        //    script.Setup(s => s.Parameters)
        //        .Returns(new List<IParameter>
        //        {
        //            parameter1.Object,
        //            parameter2.Object,
        //            parameter3.Object,
        //            parameter4.Object
        //        });

        //    var calculationEngine = new CalculationEngine(script.Object.Parameters);

        //    calculationEngine.CalculateFromText("[a]=1|[b]=2");

        //    Assert.That(parameter4.Object.Value, Is.EqualTo(7));
        //}
    }
}
