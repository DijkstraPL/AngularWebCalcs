using Build_IT_DataAccess.SteelProfiles;
using Build_IT_DataAccess.SteelProfiles.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.EntityConfigurations
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.ToTable(SteelProfilesConstants.ProfilesParameters, SteelProfilesConstants.SchemaName);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(256);
        } 
    }
}
