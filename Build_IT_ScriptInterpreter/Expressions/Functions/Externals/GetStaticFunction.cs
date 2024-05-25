using Build_IT_Data.Calculators.Interfaces;
using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using NCalc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Expressions.Functions.Externals
{
    [Export(typeof(IFunction))]
    public class GetStaticFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }

        public Func<FunctionArgs, object> Function { get; private set; }

        public ICalculator Calculator { get; private set; }

        #endregion // Properties

        #region Fields

        //private readonly IScriptRepository _scriptRepository;
        //private readonly IParameterRepository _parameterRepository;
        //private string _scriptName;
        //private string _prefix;
        //private const string _defaultPrefix = "#";

        #endregion // Fields

        #region Constructors

        //public GetStaticFunction()
        //{
        //    Set();
        //}

        //internal GetStaticFunction(
        //    IScriptRepository scriptRepository = null,
        //    IParameterRepository parameterRepository = null) : this()
        //{
        //    _scriptRepository = scriptRepository;
        //    _parameterRepository = parameterRepository;
        //}

        #endregion // Constructors

        #region Private_Methods

        //private void Set()
        //{
        //    Name = "GETSTATIC";
        //    Function = (e) =>
        //    {
        //        _scriptName = e.Parameters[0]?.Evaluate()?.ToString();

        //        _prefix = _defaultPrefix;
        //        if (_scriptName.Contains(_defaultPrefix))
        //        {
        //            _prefix = _scriptName.Substring(_scriptName.IndexOf(_defaultPrefix) + 1);
        //            _scriptName = _scriptName.Substring(0, _scriptName.IndexOf(_defaultPrefix));
        //        }

        //        IEnumerable<Parameter> staticParameters;
        //        if (Int64.TryParse(_scriptName, out long scriptId))
        //            staticParameters = GetStaticParameters(scriptId);
        //        else
        //        {
        //            var script = GetScriptFromDatabase();
        //            staticParameters = GetStaticParameters(script.Id);
        //        }

        //        if (staticParameters == null)
        //            return false;

        //        foreach(var staticParameter in staticParameters)
        //        {
        //            var parameters = e.Parameters[0].Parameters;
        //            var parameterName = _prefix + staticParameter.Name ;
        //            if (parameters.ContainsKey(parameterName))
        //                parameters[parameterName] = staticParameter.Value;
        //            else
        //                parameters.Add(parameterName, staticParameter.Value);
        //        }

        //        return true;
        //    };
        //}
           
        
        //private Script GetScriptFromDatabase()
        //{
        //    if (_scriptRepository != null)
        //    {
        //        var scriptData = _scriptRepository.GetScriptBaseOnNameAsync(_scriptName).Result;
        //        if (scriptData != null)
        //            return scriptData;
        //    }
        //    else
        //    {
        //            var scriptRepository = _scriptRepository ?? new ScriptRepository();
        //            var scriptData = scriptRepository.GetScriptBaseOnNameAsync(_scriptName).Result;
        //            if (scriptData != null)
        //                return scriptData;
        //    }

        //    throw new ArgumentException("No script with name " + _scriptName);
        //}


        //private IEnumerable<Parameter> GetStaticParameters(long scriptId)
        //{
        //    if (_parameterRepository != null)
        //    {
        //        var parameters = _parameterRepository.GetAllParametersForScriptAsync(scriptId).Result;
        //        return parameters.Where(p => (p.ParameterOptions & ParameterOptions.StaticData) != 0);
        //    }
        //    else
        //    {
        //        using (var scriptInterpreterDbContext = new ScriptInterpreterDbContext(new DbContextOptions<ScriptInterpreterDbContext>()))
        //        {
        //            var parameterRepository = _parameterRepository ?? new ParameterRepository(scriptInterpreterDbContext);
        //            var parameters = parameterRepository.GetAllParametersForScriptAsync(scriptId).Result;
        //            return parameters.Where(p => (p.ParameterOptions & ParameterOptions.StaticData) != 0);
        //        }
        //    }

        //    throw new ArgumentException("No script with id " + scriptId);
        //}

        #endregion // Private_Methods      
    }
}
