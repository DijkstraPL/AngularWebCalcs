using NUnit.Framework;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Expressions.Functions.Externals
{
    [TestFixture]
    public class GetStaticFunctionTests
    {
        //[Test]
        //public void GetStaticFunctionTest_ScriptFromFakeDatabase_Success()
        //{
        //    var scriptRepository = new Mock<IScriptRepository>();
        //    scriptRepository.Setup(sr => sr.GetScriptBaseOnNameAsync("a"))
        //        .Returns(Task.FromResult(new Script()
        //        {
        //            Id = 1,
        //            Name = "a",
        //            Description = string.Empty
        //        }));

        //    var parameterRepository = new Mock<IParameterRepository>();
        //    parameterRepository.Setup(pr => pr.GetAllParametersForScriptAsync(1))
        //    .Returns(Task.FromResult(GetParameters()));

        //    var calculateFunction = new GetStaticFunction(scriptRepository.Object, parameterRepository.Object);

        //    var parameters = new NCalc.Expression[5];
        //    parameters[0] = new NCalc.Expression("'a'");
        //    parameters[1] = new NCalc.Expression("'par1'");
        //    parameters[2] = new NCalc.Expression("1");
        //    parameters[3] = new NCalc.Expression("'par2'");
        //    parameters[4] = new NCalc.Expression("3");

        //    var functionArgs = new NCalc.FunctionArgs() { Parameters = parameters };

        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(calculateFunction.Function(functionArgs), Is.EqualTo(true));
        //        Assert.That(functionArgs.Parameters[0].Parameters.ContainsKey("#par1"));
        //        Assert.That(functionArgs.Parameters[0].Parameters.ContainsKey("#par2"));
        //        Assert.That(!functionArgs.Parameters[0].Parameters.ContainsKey("#par3"));
        //        Assert.That(functionArgs.Parameters[0].Parameters["#par1"], Is.EqualTo("7"));
        //    });
        //}

        //private IEnumerable<Parameter> GetParameters()
        //{
        //    yield return new Parameter()
        //    {
        //        Number = 1,
        //        Name = "par1",
        //        Value = "7",
        //        VisibilityValidator = null,
        //        ParameterOptions = ParameterOptions.StaticData | ParameterOptions.Visible
        //    };
        //    yield return new Parameter()
        //    {
        //        Number = 2,
        //        Name = "par2",
        //        Value = "15",
        //        VisibilityValidator = null,
        //        ParameterOptions = ParameterOptions.StaticData | ParameterOptions.Visible
        //    };
        //    yield return new Parameter()
        //    {
        //        Number = 3,
        //        Name = "par3",
        //        Value = string.Empty,
        //        VisibilityValidator = null,
        //        ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible
        //    };
        //}
    }
}
