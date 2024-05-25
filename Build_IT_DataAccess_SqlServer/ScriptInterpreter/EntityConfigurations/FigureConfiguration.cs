using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class FigureConfiguration : IEntityTypeConfiguration<Figure>
    {
        public void Configure(EntityTypeBuilder<Figure> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.Figures, ScriptInterpreterConstants.SchemaName);

            builder.HasKey(f => f.Id);

            builder.Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
