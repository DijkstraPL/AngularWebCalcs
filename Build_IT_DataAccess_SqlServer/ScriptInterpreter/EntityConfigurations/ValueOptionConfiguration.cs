using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class ValueOptionConfiguration : IEntityTypeConfiguration<ValueOption>
    {
        public void Configure(EntityTypeBuilder<ValueOption> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.ValueOptions, ScriptInterpreterConstants.SchemaName);

            builder.Property(vo => vo.Name)
                .IsRequired();

            builder.Property(p => p.Number)
                .IsRequired();

            builder.Property(vo => vo.Value)
                .IsRequired();
        }
    }
}
