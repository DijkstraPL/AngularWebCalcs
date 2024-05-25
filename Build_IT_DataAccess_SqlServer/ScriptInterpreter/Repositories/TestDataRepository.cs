using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces;
using Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Repositiories
{
    public class TestDataRepository : Repository<TestData>, ITestDataRepository
    {
        public IScriptInterpreterDbContext ScriptInterpreterContext
            => Context as IScriptInterpreterDbContext;
        
        public TestDataRepository(IScriptInterpreterDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestData>> GetAllTestDataForScriptAsync(long scriptId)
        {
            return await ScriptInterpreterContext.TestDatas
                .Where(td => td.ScriptId == scriptId)
                .Include(td => td.TestParameters)
                .Include(td => td.Assertions)
                .ToListAsync();
        }

        public async Task<TestData> GetTestDataWithAllDependanciesAsync(long testDataId)
        {
            return await ScriptInterpreterContext.TestDatas
                .Include(td => td.TestParameters)
                .Include(td => td.Assertions)
                .FirstOrDefaultAsync(td => td.Id == testDataId);
        }

        public void RemoveWithAllDependancies(TestData testData)
        {
            if (testData.Assertions != null)
                ScriptInterpreterContext.Assertions.RemoveRange(testData.Assertions);
            if (testData.TestParameters != null)
                ScriptInterpreterContext.TestParameters.RemoveRange(testData.TestParameters);
            ScriptInterpreterContext.TestDatas.Remove(testData);
        }
    }
}
