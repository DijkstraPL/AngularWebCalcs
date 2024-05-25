using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class BaseSnowLoadRoofConfiguration : IEntityTypeConfiguration<BaseSnowLoadRoof>
    {
        public void Configure(EntityTypeBuilder<BaseSnowLoadRoof> builder)
        {
            builder.ToTable(SnowLoadsConstants.BaseSnowLoadRoof, SnowLoadsConstants.SchemaName);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ThermalCoefficient)
                        .IsRequired(false);
            builder.Property(a => a.OverallHeatTransferCoefficient)
                .IsRequired(false);
            builder.Property(a => a.InternalTemperature)
                .IsRequired(false);
            builder.Property(a => a.TempreatureDifference)
                .IsRequired(false);
            builder.Property(a => a.AltitudeAboveSea)
                .IsRequired(false);
            builder.Property(a => a.CurrentZone)
                .IsRequired();
            builder.Property(a => a.CurrentTopography)
                .IsRequired();
            builder.Property(a => a.SnowLoadId)
                .IsRequired();
        }
    }
}
