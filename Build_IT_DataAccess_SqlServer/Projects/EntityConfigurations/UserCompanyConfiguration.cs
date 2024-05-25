using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class UserCompanyConfiguration : IEntityTypeConfiguration<UserCompany>
    {
        public void Configure(EntityTypeBuilder<UserCompany> builder)
        {
            builder.ToTable(ProjectConstants.UserCompanies, ProjectConstants.SchemaName);

            builder.HasKey(p => new { p.UserId, p.CompanyId});
        }
    }
}
