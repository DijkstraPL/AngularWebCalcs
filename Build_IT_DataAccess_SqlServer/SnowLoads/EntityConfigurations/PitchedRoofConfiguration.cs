using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class PitchedRoofConfiguration :IEntityTypeConfiguration<PitchedRoof>
    {
        public void Configure(EntityTypeBuilder<PitchedRoof> builder)
        {
            builder.ToTable(SnowLoadsConstants.PitchedRoofs, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.LeftSlope)
                .IsRequired();
            builder.Property(a => a.LeftSnowFences)
                .IsRequired();
            builder.Property(a => a.RightSlope)
                .IsRequired();
            builder.Property(a => a.RightSnowFences)
                .IsRequired();
        }
    }
}
