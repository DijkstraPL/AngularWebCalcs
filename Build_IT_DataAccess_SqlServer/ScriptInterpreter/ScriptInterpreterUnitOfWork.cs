using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter
{
    public class ScriptInterpreterUnitOfWork : IScriptInterpreterUnitOfWork
    {
        public IScriptRepository Scripts { get; }
        public IParameterRepository Parameters { get; }
        public ITagRepository Tags { get; }

        private readonly IScriptInterpreterDbContext _context;

        public ScriptInterpreterUnitOfWork(
            IScriptInterpreterDbContext context,
            IScriptRepository scriptRepository,
            IParameterRepository parameterRepository,
            ITagRepository tagRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Scripts = scriptRepository ?? throw new ArgumentNullException(nameof(scriptRepository));
            Parameters = parameterRepository ?? throw new ArgumentNullException(nameof(parameterRepository));
            Tags = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
