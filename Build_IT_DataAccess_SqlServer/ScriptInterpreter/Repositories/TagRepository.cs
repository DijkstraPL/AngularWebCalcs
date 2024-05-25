using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext
            => Context as IScriptInterpreterDbContext;

        public TagRepository(IScriptInterpreterDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Tag>> GetTagsForScriptAsync(long scriptId)
        {
            var script = await ScriptInterpreterContext.Scripts.FindAsync(scriptId);
            var tagsIds = script.Tags.Select(st => st.TagId);
            return await ScriptInterpreterContext.Tags.Where(t => tagsIds.Contains(t.Id)).ToListAsync();
        }
    }
}
