using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces
{
    public interface IScriptInterpreterUnitOfWork : IUnitOfWork
    {
        IScriptRepository Scripts { get; }
        IParameterRepository Parameters { get; }
        ITagRepository Tags { get; }
    }
}
