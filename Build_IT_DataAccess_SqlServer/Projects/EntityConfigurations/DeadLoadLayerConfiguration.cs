using Build_IT_DataAccess.Projects;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.Projects.EntityConfigurations
{
    public class DeadLoadLayerConfiguration : IEntityTypeConfiguration<DeadLoadLayer>
    {
        public void Configure(EntityTypeBuilder<DeadLoadLayer> builder)
        {
            builder.ToTable(ProjectConstants.DeadLoadLayers, ProjectConstants.SchemaName);

            builder.HasKey(p => p.Id);
        }
    }
}
