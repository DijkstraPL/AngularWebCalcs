using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ParameterInputConfiguration : IEntityTypeConfiguration<ParameterInput>
    {
        public void Configure(EntityTypeBuilder<ParameterInput> builder)
        {
            builder.ToTable(ProjectConstants.ParameterInputs, ProjectConstants.SchemaName);

            builder.HasKey(p => new { p.ParameterId, p.ScriptDatatId });


            builder.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}
