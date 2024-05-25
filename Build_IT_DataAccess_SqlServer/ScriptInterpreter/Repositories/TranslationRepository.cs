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
    public class TranslationRepository : ITranslationRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext { get; }

        public TranslationRepository(IScriptInterpreterDbContext context)
        {
            ScriptInterpreterContext = context;
        }

        public async Task<IEnumerable<ScriptTranslation>> GetScriptTranslations(long scriptId)
        {
            return await ScriptInterpreterContext.ScriptsTranslations
                .Where(st => st.ScriptId == scriptId)
                .ToListAsync();
        }
        public async Task<ScriptTranslation> GetScriptTranslation(long scriptId, Language language)
        {
            return await ScriptInterpreterContext.ScriptsTranslations
                 .SingleOrDefaultAsync(st => st.ScriptId == scriptId && st.Language == language);
        }
        public async Task AddScriptTranslationAsync(ScriptTranslation scriptTranslation)
        {
            await ScriptInterpreterContext.ScriptsTranslations.AddAsync(scriptTranslation);
        }
        public void RemoveScriptTranslation(ScriptTranslation scriptTranslation)
        {
            ScriptInterpreterContext.ScriptsTranslations.Remove(scriptTranslation);
        }


        public async Task<IEnumerable<ParameterTranslation>> GetParametersTranslations(long scriptId, Language language)
        {
            var parametersIds = await ScriptInterpreterContext.Parameters.Where(p => p.ScriptId == scriptId).Select(p => p.Id).ToListAsync();
            return await ScriptInterpreterContext.ParametersTranslations
                .Where(pt => parametersIds.Contains(pt.ParameterId) && pt.Language == language).ToListAsync();
        }
        public async Task<ParameterTranslation> GetParameterTranslation(long parameterId, Language language)
        {
            return await ScriptInterpreterContext.ParametersTranslations
                .SingleOrDefaultAsync(pt => pt.ParameterId == parameterId && pt.Language == language);
        }
        public async Task<ParameterTranslation> GetParameterTranslation(long id)
        {
            return await ScriptInterpreterContext.ParametersTranslations.FindAsync(id);
        }
        public async Task AddParameterTranslationAsync(ParameterTranslation parameterTranslation)
        {
            await ScriptInterpreterContext.ParametersTranslations.AddAsync(parameterTranslation);
        }

        public void RemoveParameterTranslation(ParameterTranslation parameterTranslation)
        {
            ScriptInterpreterContext.ParametersTranslations.Remove(parameterTranslation);
        }


        public async Task<IEnumerable<ValueOptionTranslation>> GetValueOptionsTranslations(long parameterId, Language language)
        {
            var parameter = await ScriptInterpreterContext.Parameters
                .Include(p => p.ValueOptions)
                .FirstOrDefaultAsync(p => p.Id == parameterId);
            var valueOptionsIds = parameter.ValueOptions.Select(vo => vo.Id);
            return await ScriptInterpreterContext.ValueOptionsTranslations
                .Where(vot => valueOptionsIds.Contains(vot.ValueOptionId) && vot.Language == language).ToListAsync();
        }
        public async Task<ValueOptionTranslation> GetValueOptionTranslation(long valueOptionId, Language language)
        {
            return await ScriptInterpreterContext.ValueOptionsTranslations
                .SingleOrDefaultAsync(pt => pt.ValueOptionId == valueOptionId && pt.Language == language);
        }
        public async Task<ValueOptionTranslation> GetValueOptionTranslation(long id)
        {
            return await ScriptInterpreterContext.ValueOptionsTranslations.FindAsync(id);
        }
        public async Task AddValueOptionTranslationAsync(ValueOptionTranslation valueOptionTranslation)
        {
            await ScriptInterpreterContext.ValueOptionsTranslations.AddAsync(valueOptionTranslation);
        }
        public void RemoveValueOptionTranslation(ValueOptionTranslation valueOptionTranslation)
        {
            ScriptInterpreterContext.ValueOptionsTranslations.Remove(valueOptionTranslation);
        }

        public async Task<IEnumerable<ParameterGroupTranslation>> GetGroupTranslations(long scriptId, Language language)
        {
            var groupsIds = ScriptInterpreterContext.Groups
                .Where(g => g.ScriptId == scriptId)
                .Select(g => g.Id);
            return await ScriptInterpreterContext.ParameterGroupsTranslations
                          .Where(gt => groupsIds.Contains(gt.GroupId) && gt.Language == language)
                          .ToListAsync();
        }

        public async Task<ParameterGroupTranslation> GetGroupTranslation(long groupId)
        {
            return await ScriptInterpreterContext.ParameterGroupsTranslations
                .FirstOrDefaultAsync(gt => gt.GroupId == groupId);
        }
        public async Task<ParameterGroupTranslation> GetGroupTranslation(long groupId, Language language)
        {
            return await ScriptInterpreterContext.ParameterGroupsTranslations
                .FirstOrDefaultAsync(gt => gt.GroupId == groupId && gt.Language == language);
        }

        public async Task AddGroupTranslationAsync(ParameterGroupTranslation groupTranslation)
        {
            await ScriptInterpreterContext.ParameterGroupsTranslations.AddAsync(groupTranslation);
        }

        public void RemoveGroupTranslation(ParameterGroupTranslation groupTranslation)
        {
            ScriptInterpreterContext.ParameterGroupsTranslations.Remove(groupTranslation);
        }
    }
}
