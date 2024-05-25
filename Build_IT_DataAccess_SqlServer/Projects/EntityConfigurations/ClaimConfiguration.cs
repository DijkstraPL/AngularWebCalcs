using Build_IT_DataAccess.DeadLoads;
using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable(ProjectConstants.Claims, ProjectConstants.SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(128);


        }
    }
}
