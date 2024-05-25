using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(ProjectConstants.Projects, ProjectConstants.SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(2048);
        }
    }
}
