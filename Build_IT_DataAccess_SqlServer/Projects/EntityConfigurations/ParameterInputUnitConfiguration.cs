using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ParameterInputUnitConfiguration : IEntityTypeConfiguration<ParameterInputUnit>
    {
        public void Configure(EntityTypeBuilder<ParameterInputUnit> builder)
        {
            builder.ToTable(ProjectConstants.ParameterInputUnits, ProjectConstants.SchemaName);

            builder.HasKey(p => new { p.ParameterInputId, p.UnitId });


            builder.Property(a => a.Power)
                .HasDefaultValue(1d);
        }
    }
}
