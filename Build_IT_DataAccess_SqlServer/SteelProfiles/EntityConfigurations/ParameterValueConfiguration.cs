using Build_IT_DataAccess.SteelProfiles.Entities;
using Build_IT_DataAccess.SteelProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.EntityConfigurations
{
    public class ParameterValueConfiguration : IEntityTypeConfiguration<ParameterValue>
    {
        public void Configure(EntityTypeBuilder<ParameterValue> builder)
        {
            builder.ToTable(SteelProfilesConstants.ProfilesParameterValues, SteelProfilesConstants.SchemaName);

            builder.Property(pv => pv.Value)
                .IsRequired()
                .HasMaxLength(64);
            builder.HasOne(pv => pv.Parameter);
        }
    }
}
