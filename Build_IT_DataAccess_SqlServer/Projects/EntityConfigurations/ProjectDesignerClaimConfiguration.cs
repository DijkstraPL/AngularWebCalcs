using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ProjectDesignerClaimConfiguration : IEntityTypeConfiguration<ProjectDesignerClaim>
    {
        public void Configure(EntityTypeBuilder<ProjectDesignerClaim> builder)
        {
            builder.ToTable(ProjectConstants.ProjectDesignerClaims, ProjectConstants.SchemaName);
            builder.HasOne(d => d.Designer).WithMany().HasForeignKey(d => d.DesignerId);
            builder.HasKey(p => new { p.DesignerId, p.ProjectId, p.ClaimId });
        }
    }
}
