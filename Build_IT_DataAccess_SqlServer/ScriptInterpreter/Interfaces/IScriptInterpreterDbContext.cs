using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using Microsoft.EntityFrameworkCore;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.Interfaces
{
    public interface IScriptInterpreterDbContext : IDbContext
    {
        DbSet<Script> Scripts { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Parameter> Parameters { get; set; }
        DbSet<Figure> Figures { get; set; }
        DbSet<ParameterGroup> Groups { get; set; }
        DbSet<Unit> Units { get; set; }
        DbSet<Assertion> Assertions { get; set; }
        DbSet<TestParameter> TestParameters { get; set; }
        DbSet<TestData> TestDatas { get; set; }

        DbSet<ScriptTranslation> ScriptsTranslations { get; set; }
        DbSet<ParameterTranslation> ParametersTranslations { get; set; }
        DbSet<ValueOptionTranslation> ValueOptionsTranslations { get; set; }
       DbSet<ScriptGroupTranslation> ScriptGroupsTranslations { get; set; }
       DbSet<ParameterGroupTranslation> ParameterGroupsTranslations { get; set; }
       DbSet<UnitTranslation> UnitTranslations { get; set; }
    }
}
