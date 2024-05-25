using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class SnowguardsConfiguration :IEntityTypeConfiguration<Snowguards>
    {
        public void Configure(EntityTypeBuilder<Snowguards> builder)
        {
            builder.ToTable(SnowLoadsConstants.Snowguards, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.SnowLoadOnRoofValue)
                .IsRequired();
            builder.Property(a => a.Width)
                .IsRequired();
            builder.Property(a => a.Slope)
                .IsRequired();
        }
    }
}
