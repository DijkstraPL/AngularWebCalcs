using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ProjectDeadLoadConfiguration : IEntityTypeConfiguration<ProjectDeadLoad>
    {
        public void Configure(EntityTypeBuilder<ProjectDeadLoad> builder)
        {
            builder.ToTable(ProjectConstants.ProjectDeadLoads, ProjectConstants.SchemaName);

            builder.HasKey(p => new { p.ProjectId, p.DeadLoadId });
        }
    }
}
