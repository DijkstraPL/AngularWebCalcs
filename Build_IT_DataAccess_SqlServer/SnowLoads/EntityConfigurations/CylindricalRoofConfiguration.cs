using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class CylindricalRoofConfiguration : IEntityTypeConfiguration<CylindricalRoof>
    {
        public void Configure(EntityTypeBuilder<CylindricalRoof> builder)
        {
            builder.ToTable(SnowLoadsConstants.CylindricalRoofs, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.Width)
                .IsRequired();
            builder.Property(a => a.Height)
                .IsRequired();
            builder.Property(a => a.DriftLength)
                .IsRequired();
        }
    }
}
