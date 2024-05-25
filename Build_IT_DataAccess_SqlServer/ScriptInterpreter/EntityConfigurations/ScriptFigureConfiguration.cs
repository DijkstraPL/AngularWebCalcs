using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ScriptFigureConfiguration : IEntityTypeConfiguration<ScriptFigure>
    {
        public void Configure(EntityTypeBuilder<ScriptFigure> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ScriptFigures, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(st
                => new { st.ScriptId, st.FigureId });
        }
    }
}
