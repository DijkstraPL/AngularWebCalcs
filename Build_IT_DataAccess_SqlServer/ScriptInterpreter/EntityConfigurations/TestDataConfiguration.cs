using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess;

namespace Build_IT_DataAccess_SqlServer.ScriptInterpreter.EntityConfigurations
{
    public class TestDataConfiguration : IEntityTypeConfiguration<TestData>
    {
        public void Configure(EntityTypeBuilder<TestData> builder)
        {
            builder.ToTable(ScriptInterpreterConstants.TestDatas, ScriptInterpreterConstants.SchemaName);

            builder.Property(td => td.Name)
                .HasMaxLength(255);

            builder.HasOne<Script>(td => td.Script);

            builder.HasMany<TestParameter>(td => td.TestParameters)
                .WithOne(st => st.TestData)
                .HasForeignKey(st => st.TestDataId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<Assertion>(td => td.Assertions)
                .WithOne(st => st.TestData)
                .HasForeignKey(st => st.TestDataId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
