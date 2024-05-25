using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories
{
    public class GroupRepository : Repository<ParameterGroup>, IGroupRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext
        => Context as IScriptInterpreterDbContext;

        public GroupRepository(IScriptInterpreterDbContext context)
            : base(context)
        {
        }
    }
}
