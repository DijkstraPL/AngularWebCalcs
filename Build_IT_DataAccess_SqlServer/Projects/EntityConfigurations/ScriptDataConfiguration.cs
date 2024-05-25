using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ScriptDataConfiguration : IEntityTypeConfiguration<ScriptData>
    {
        public void Configure(EntityTypeBuilder<ScriptData> builder)
        {
            builder.ToTable(ProjectConstants.ScriptDatas, ProjectConstants.SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
