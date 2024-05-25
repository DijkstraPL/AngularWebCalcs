using Build_IT_DataAccess.SteelProfiles.Entities;
using Build_IT_DataAccess.SteelProfiles.Entities.Enums;
using Build_IT_DataAccess.SteelProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.EntityConfigurations
{
    public class SectionPointConfiguration : IEntityTypeConfiguration<SectionPoint>
    {
        public void Configure(EntityTypeBuilder<SectionPoint> builder)
        {
            builder.ToTable(SteelProfilesConstants.SectionPoints, SteelProfilesConstants.SchemaName);

            builder.Property(pt => pt.X)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(pt => pt.Y)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(pt => pt.ChamferX)
                .HasMaxLength(16);
            builder.Property(pt => pt.ChamferY)
                .HasMaxLength(16);
            builder.Property(pt => pt.ChamferType)
                .HasDefaultValue(ChamferType.None);
        }
    }
}
