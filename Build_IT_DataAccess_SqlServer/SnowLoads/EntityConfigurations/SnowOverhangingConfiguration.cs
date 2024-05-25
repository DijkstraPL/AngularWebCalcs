using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class SnowOverhangingConfiguration : IEntityTypeConfiguration<SnowOverhanging>
    {
        public void Configure(EntityTypeBuilder<SnowOverhanging> builder)
        {
            builder.ToTable(SnowLoadsConstants.SnowOverhangings, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.SnowLayerDepth)
                .IsRequired();
            builder.Property(a => a.SnowFences)
                .IsRequired();
            builder.Property(a => a.Slope)
                .IsRequired();
        }
    }
}
