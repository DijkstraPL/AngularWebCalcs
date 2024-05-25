using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class TestParameterConfiguration : IEntityTypeConfiguration<TestParameter>
    {
        public void Configure(EntityTypeBuilder<TestParameter> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.TestParameters, ScriptInterpreterConstants.SchemaName);

            builder.Property(tp => tp.Value)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(tp => tp.ParameterUnit);

            builder.HasKey(tp => new { tp.ParameterUnitId, tp.TestDataId });
        }
    }
}
