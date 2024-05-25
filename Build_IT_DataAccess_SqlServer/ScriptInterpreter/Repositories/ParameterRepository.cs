using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories
{
    public class ParameterRepository : Repository<Parameter>, IParameterRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext
        => Context as IScriptInterpreterDbContext;

        public ParameterRepository(IScriptInterpreterDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Parameter>> GetAllParametersForScriptAsync(long scriptId)
        {
            return await ScriptInterpreterContext.Parameters
                .Where(p => p.ScriptId == scriptId)
                .Include(p => p.ParameterGroup)
                .Include(p => p.ValueOptions)
                .Include(p => p.ParameterFigures)
                .ThenInclude(p => p.Figure)
                .OrderBy(p => p.Number)
                .ToListAsync();
        }

        public async Task<Parameter> GetParameterWithAllDependanciesAsync(long parameterId)
        {
            return await ScriptInterpreterContext.Parameters
                .Include(p => p.ParameterGroup)
                .Include(p => p.ValueOptions)
                .Include(p => p.ParameterFigures)
                .ThenInclude(p => p.Figure)
                .SingleOrDefaultAsync(p => p.Id == parameterId);
        }

        public async Task<IEnumerable<Figure>> GetFiguresAsync(long parameterId)
        {
            var parameter = await ScriptInterpreterContext.Parameters
                .Include(p => p.ParameterFigures)
                .ThenInclude(pf => pf.Figure)
                .SingleOrDefaultAsync(p => p.Id == parameterId);
            return parameter.ParameterFigures.Select(pf => pf.Figure);
        }
    }
}
