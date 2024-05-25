using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class DriftingAtProjectionsObstructionsConfiguration : IEntityTypeConfiguration<DriftingAtProjectionsObstructions>
    {
        public void Configure(EntityTypeBuilder<DriftingAtProjectionsObstructions> builder)
        {
            builder.ToTable(SnowLoadsConstants.DriftingsAtProjectionsObstructions, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.DriftLength)
                .IsRequired();
            builder.Property(a => a.ObstructionHeight)
                .IsRequired();
        }
    }
}
