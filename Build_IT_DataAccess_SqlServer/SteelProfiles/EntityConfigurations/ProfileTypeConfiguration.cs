using Build_IT_DataAccess.SteelProfiles.Entities;
using Build_IT_DataAccess.SteelProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.EntityConfigurations
{
    public class ProfileTypeConfiguration : IEntityTypeConfiguration<ProfileType>
    {
        public void Configure(EntityTypeBuilder<ProfileType> builder)
        {
            builder.ToTable(SteelProfilesConstants.ProfileTypes, SteelProfilesConstants.SchemaName);

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasMany<Parameter>(pt => pt.Parameters);
            builder.HasMany<SectionPoint>(pt => pt.SectionPoints);
            builder.HasMany<SteelProfile>(pt => pt.SteelProfiles);
        }
    }
}
