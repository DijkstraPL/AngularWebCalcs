using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class DesignerConfiguration : IEntityTypeConfiguration<Designer>
    {
        public void Configure(EntityTypeBuilder<Designer> builder)
        {
            builder.ToTable(ProjectConstants.Designers, ProjectConstants.SchemaName);

            builder.HasKey(p => p.UserId);
        }
    }
}
