using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ValueOptionFigureConfiguration : IEntityTypeConfiguration<ValueOptionFigure>
    {
        public void Configure(EntityTypeBuilder<ValueOptionFigure> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ValueOptionFigures, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(st
                => new { st.ValueOptionId, st.FigureId });
        }
    }
}
