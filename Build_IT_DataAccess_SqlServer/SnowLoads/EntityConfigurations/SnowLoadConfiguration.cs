using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class SnowLoadConfiguration :  IEntityTypeConfiguration<SnowLoad>
    {
        public void Configure(EntityTypeBuilder<SnowLoad> builder)
        {
            builder.ToTable(SnowLoadsConstants.SnowLoads, SnowLoadsConstants.SchemaName);
            
            builder.HasOne(a => a.SnowLoadRoof)
                    .WithOne(a => a.SnowLoad)
                    .HasForeignKey<BaseSnowLoadRoof>(c => c.SnowLoadId);

            builder.Property(a => a.Name)
                .HasMaxLength(124)
                .IsRequired();
        }
    }
}
