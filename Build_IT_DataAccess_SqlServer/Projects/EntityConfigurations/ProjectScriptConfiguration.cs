using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class ProjectScriptConfiguration : IEntityTypeConfiguration<ProjectScript>
    {
        public void Configure(EntityTypeBuilder<ProjectScript> builder)
        {
            builder.ToTable(ProjectConstants.ProjectScripts, ProjectConstants.SchemaName);

            builder.HasKey(p => new { p.ScriptId, p.ProjectId });
        }
    }
}
