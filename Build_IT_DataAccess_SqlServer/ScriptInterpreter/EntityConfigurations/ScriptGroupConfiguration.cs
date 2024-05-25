using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ScriptGroupConfiguration : IEntityTypeConfiguration<ScriptGroup>
    {
        public void Configure(EntityTypeBuilder<ScriptGroup> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ScriptGroups, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(sg => sg.Id);
            builder.Property(sg => sg.Name)
                .HasMaxLength(256);
        }
    }
}
