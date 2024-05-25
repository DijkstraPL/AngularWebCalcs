using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories
{
    public class ScriptRepository : Repository<Script>, IScriptRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext 
            => Context as IScriptInterpreterDbContext;

        public ScriptRepository(IScriptInterpreterDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Script>> GetAllScriptsWithTagsAsync()
        {
            return await ScriptInterpreterContext.Scripts
                .Include(s => s.Tags)
                .ThenInclude(t => t.Tag)
                .Include(s => s.PreviousScript)
                .ToListAsync();
        }

        public async Task<Script> GetScriptWithTagsAsync(long id)
        {
            return await ScriptInterpreterContext.Scripts
                .Include(s => s.Tags)
                .ThenInclude(s => s.Tag)
                .Include(s => s.PreviousScript)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Script> GetScriptBaseOnNameAsync(string name)
        {
            return await ScriptInterpreterContext.Scripts
                .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<ICollection<ScriptTranslation>> GetScriptTranslations(long scriptId)
        {
            return await ScriptInterpreterContext.ScriptsTranslations
                .Where(st => st.ScriptId == scriptId)
                .ToListAsync();
        }
    }
}
