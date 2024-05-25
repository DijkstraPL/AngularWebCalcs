namespace Build_IT_ScriptInterpreter.Expressions.Functions.Externals
{
    //[Export(typeof(IFunction))]
    //public class CalculateFunction : IFunction
    //{
    //    #region Properties

    //    public string Name { get; private set; }

    //    public Func<FunctionArgs, object> Function { get; private set; }

    //    public ICalculator Calculator { get; private set; }

    //    #endregion // Properties

    //    #region Fields

    //    private string _scriptName;
    //    private string _prefix;
    //    private const string _defaultPrefix = "#";
    //    private readonly IScriptRepository _scriptRepository;
    //    private readonly IParameterRepository _parameterRepository;
    //    private readonly bool _useContainer = true;

    //    #endregion // Fields

    //    #region Constructors

    //    public CalculateFunction()
    //    {
    //        Set();
    //    }

    //    internal CalculateFunction(
    //        IScriptRepository scriptRepository = null,
    //        IParameterRepository parameterRepository = null,
    //        bool useContainer = false,
    //        ICalculator calculator = null) : this()
    //    {
    //        _scriptRepository = scriptRepository;
    //        _parameterRepository = parameterRepository;
    //        _useContainer = useContainer;
    //        Calculator = calculator;
    //    }

    //    #endregion // Constructors

    //    #region Private_Methods

    //    private void Set()
    //    {
    //        Name = "CALCULATE";
    //        Function = (e) =>
    //        {
    //            _prefix = _defaultPrefix;
    //            _scriptName = e.Parameters[0]?.Evaluate()?.ToString();
    //            if (_scriptName.Contains(_defaultPrefix))
    //            {
    //                _prefix = _scriptName.Substring(_scriptName.IndexOf(_defaultPrefix) +1);
    //                _scriptName = _scriptName.Substring(0, _scriptName.IndexOf(_defaultPrefix));
    //            }

    //            if (_useContainer)
    //                Compose(_scriptName);
    //            if (Calculator != null)
    //                return CalculateService(e);

    //            return CalculateScriptFromDatabase(e);
    //        };
    //    }

    //    private void Compose(string calculatorName)
    //    {
    //        var configuration = new ContainerConfiguration()
    //           .WithAssembly(typeof(ServiceStartup).GetTypeInfo().Assembly);

    //        using (var container = configuration.CreateContainer())
    //        {
    //            if (container.TryGetExport(calculatorName, out ICalculator calculator))
    //                Calculator = calculator;
    //        }
    //    }

    //    private object CalculateService(FunctionArgs functionArgs)
    //    {
    //        IList<object> arguments = new List<object>();
    //        for (int i = 1; i < functionArgs.Parameters.Length; i++)
    //            try
    //            {
    //                var result = functionArgs.Parameters[i].Evaluate();
    //                if (result == null)
    //                    arguments.RemoveAt(arguments.Count - 1);
    //                else
    //                    arguments.Add(result);
    //            }
    //            catch (ArgumentException)
    //            {
    //                arguments.RemoveAt(arguments.Count - 1);
    //            }

    //        Calculator.Map(arguments);
    //        return SetParameters(Calculator.Calculate(), functionArgs);
    //    }

    //    private object SetParameters(IResult result, FunctionArgs functionArgs)
    //    {
    //        foreach (var property in result.GetProperties())
    //            functionArgs.Parameters.First().Parameters.Add($"{_prefix}{property.Key}", property.Value);

    //        return true;
    //    }

    //    private object CalculateScriptFromDatabase(FunctionArgs e)
    //    {
    //        bool isNumber = Int64.TryParse(_scriptName, out long scriptId);

    //        if (_scriptRepository != null && _parameterRepository != null)
    //        {
    //            SI.Script scriptData;
    //            if (isNumber)
    //                scriptData = _scriptRepository.GetAsync(scriptId).Result;
    //            else
    //                scriptData = _scriptRepository.GetScriptBaseOnNameAsync(_scriptName).Result;
    //            if (scriptData != null)
    //                return CalculateScript(scriptData, e, _parameterRepository);
    //        }
    //        else
    //        {
    //            using (var scriptInterpreterDbContext = new ScriptInterpreterDbContext(new DbContextOptions<ScriptInterpreterDbContext>()))
    //            {
    //                var scriptRepository = _scriptRepository ?? new ScriptRepository();
    //                SI.Script scriptData;
    //                if (isNumber)
    //                    scriptData = _scriptRepository.GetAsync(scriptId).Result;
    //                else
    //                    scriptData = _scriptRepository.GetScriptBaseOnNameAsync(_scriptName).Result;
    //                if (scriptData != null)
    //                {
    //                    var parameterRepository = _parameterRepository ?? new ParameterRepository(scriptInterpreterDbContext);
    //                    return CalculateScript(scriptData, e, parameterRepository);
    //                }
    //            }
    //        }

    //        throw new ArgumentException("No script with name " + _scriptName);
    //    }


    //    private object CalculateScript(SI.Script scriptData, FunctionArgs functionArgs, IParameterRepository parameterRepository)
    //    {
    //        var script = ScriptBuilder.Create(
    //                scriptData.Name,
    //                scriptData.Description,
    //                new string[0])
    //                .Build();

    //        script.Parameters = GetParameters(scriptData, parameterRepository).ToList();

    //        var calculationEngine = new CalculationEngine(script.Parameters);

    //        IDictionary<string, object> parameterValues = GetParametersValues(functionArgs);

    //        calculationEngine.Calculate(parameterValues);
    //        IncludeParameterValues(script, parameterValues);

    //        SetPrefixes(functionArgs, script);

    //        return true;
    //    }


    //    private IEnumerable<IParameter> GetParameters(SI.Script scriptData, IParameterRepository parameterRepository)
    //    {
    //        var parametersData = parameterRepository.GetAllParametersForScriptAsync(scriptData.Id).Result;
    //        foreach (var parameter in parametersData)
    //        {
    //            yield return new SIP.Parameter()
    //            {
    //                Number = parameter.Number,
    //                Name = parameter.Name,
    //                Value = parameter.Value,
    //                VisibilityValidator = parameter.VisibilityValidator,
    //                ParameterOptions = (SIP.ParameterOptions)parameter.ParameterOptions
    //            };
    //        }
    //    }

    //    private IDictionary<string, object> GetParametersValues(FunctionArgs functionArgs)
    //    {
    //        IDictionary<string, object> parameterValues = new Dictionary<string, object>();
    //        for (int i = 1; i < functionArgs.Parameters.Length; i += 2)
    //            parameterValues.Add(functionArgs.Parameters[i].Evaluate().ToString(), functionArgs.Parameters[i + 1].Evaluate());
    //        return parameterValues;
    //    }

    //    private static void IncludeParameterValues(Scripts.Interfaces.IScript script, IDictionary<string, object> parameterValues)
    //    {
    //        script.Parameters.RemoveAll(p => !parameterValues.ContainsKey(p.Name));

    //        foreach (var par in parameterValues)
    //        {
    //            var pp = script.Parameters.SingleOrDefault(p => p.Name == par.Key);
    //            if (pp != null)
    //                pp.Value = par.Value.ToString();
    //        }
    //    }

    //    private void SetPrefixes(FunctionArgs functionArgs, Scripts.Interfaces.IScript script)
    //    {
    //        foreach (var par in script.Parameters.Where(p => (p.Context & SIP.ParameterOptions.Calculation) != 0))
    //            functionArgs.Parameters.First().Parameters.Add($"{_prefix}{par.Name}", par.Value);
    //    }

    //    #endregion // Private_Methods      
    //}
}
