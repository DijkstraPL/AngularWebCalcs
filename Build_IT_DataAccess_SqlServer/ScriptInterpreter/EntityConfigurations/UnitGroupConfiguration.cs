using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class UnitGroupConfiguration : IEntityTypeConfiguration<UnitGroup>
    {
        public void Configure(EntityTypeBuilder<UnitGroup> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.UnitGroups, ScriptInterpreterConstants.SchemaName);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
