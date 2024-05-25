using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ViewConfiguration : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.Views, ScriptInterpreterConstants.SchemaName);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(t => t.ViewDefinition)
                .IsRequired();
        }
    }
}
