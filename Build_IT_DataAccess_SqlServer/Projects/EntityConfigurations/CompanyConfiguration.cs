using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(ProjectConstants.Companies, ProjectConstants.SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
