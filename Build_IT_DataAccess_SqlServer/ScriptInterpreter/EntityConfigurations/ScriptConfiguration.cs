using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ScriptConfiguration : IEntityTypeConfiguration<Script>
    {
        public void Configure(EntityTypeBuilder<Script> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.Scripts, ScriptInterpreterConstants.SchemaName);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(2048);
            builder.Property(s => s.Notes)
                .HasMaxLength(2048);

            builder.HasMany<ScriptTag>(s => s.Tags)
                .WithOne(st => st.Script)
                .HasForeignKey(st => st.ScriptId);

            builder.Property(s => s.GroupName)
                .HasMaxLength(255);

            builder.Property(s => s.Author)
                .HasMaxLength(255);
            
            builder.Property(s => s.AccordingTo)
                .HasMaxLength(255);

            builder.HasMany<Parameter>(s => s.Parameters)
                .WithOne(p => p.Script)
                .HasForeignKey(p => p.ScriptId);

            builder.Property(s => s.IsPublic)
                .HasDefaultValue(false);
        }
    }
}
