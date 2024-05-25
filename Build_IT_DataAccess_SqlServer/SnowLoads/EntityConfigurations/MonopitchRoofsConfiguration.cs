using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class MonopitchRoofsConfiguration :IEntityTypeConfiguration<MonopitchRoof>
    {
        public void Configure(EntityTypeBuilder<MonopitchRoof> builder)
        {
            builder.ToTable(SnowLoadsConstants.MonopitchRoofs, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.Slope)
                .IsRequired();
            builder.Property(a => a.SnowFences)
                .IsRequired();
        }
    }
}
