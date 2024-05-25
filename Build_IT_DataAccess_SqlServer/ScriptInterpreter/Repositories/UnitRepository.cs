using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext
            => Context as IScriptInterpreterDbContext;

        public UnitRepository(IScriptInterpreterDbContext context) : base(context)
        {
        }
    }
}
