using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.Units, ScriptInterpreterConstants.SchemaName);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(t => t.Description)
                .HasMaxLength(1024);
        }
    }
}
