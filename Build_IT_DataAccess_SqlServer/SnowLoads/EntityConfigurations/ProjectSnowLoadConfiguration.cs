using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class ProjectSnowLoadConfiguration : IEntityTypeConfiguration<ProjectSnowLoad>
    {
        public void Configure(EntityTypeBuilder<ProjectSnowLoad> builder)
        {
            builder.ToTable(SnowLoadsConstants.ProjectSnowLoads, SnowLoadsConstants.SchemaName);

            builder.HasKey(p => new { p.ProjectId, p.SnowLoadId });
        }
    }
}
