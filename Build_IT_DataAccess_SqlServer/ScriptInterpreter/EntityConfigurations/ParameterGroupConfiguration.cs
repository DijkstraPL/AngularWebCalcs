using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ParameterGroupConfiguration : IEntityTypeConfiguration<ParameterGroup>
    {
        public void Configure(EntityTypeBuilder<ParameterGroup> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ParameterGroups, ScriptInterpreterConstants.SchemaName);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(g => g.VisibilityValidator)
                .HasMaxLength(512);

            builder.HasMany<Parameter>(g => g.Parameters)
                .WithOne(p => p.ParameterGroup)
                .HasForeignKey(p => p.ParameterGroupId);

            builder.HasOne<Script>(g => g.Script);
        }
    }
}
