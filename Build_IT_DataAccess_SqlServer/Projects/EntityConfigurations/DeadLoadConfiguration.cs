using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class DeadLoadConfiguration : IEntityTypeConfiguration<DeadLoad >
    {
        public void Configure(EntityTypeBuilder<DeadLoad > builder)
        {
            builder.ToTable(ProjectConstants.DeadLoads, ProjectConstants.SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
