using Build_IT_DataAccess;
using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class AssertionConfiguration : IEntityTypeConfiguration<Assertion>
    {
        public void Configure(EntityTypeBuilder<Assertion> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.Assertions, ScriptInterpreterConstants.SchemaName);

            builder.Property(s => s.Value)
                .IsRequired();
        }
    }
}
