using Build_IT_DataAccess.SteelProfiles.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Build_IT_DataAccess.SteelProfiles;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.EntityConfigurations
{
    public class SteelProfileConfiguration : IEntityTypeConfiguration<SteelProfile>
    {
        public void Configure(EntityTypeBuilder<SteelProfile> builder)
        {
            builder.ToTable(SteelProfilesConstants.SteelProfiles, SteelProfilesConstants.SchemaName);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(32);
            builder.HasMany(sp => sp.ParametersValues);
        }
    }
}
