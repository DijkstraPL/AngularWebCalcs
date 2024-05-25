using Build_IT_DataAccess.SnowLoads;
using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.EntityConfigurations
{
    public class RoofAbuttingToTallerConstructionConfiguration :  IEntityTypeConfiguration<RoofAbuttingToTallerConstruction>
    {
        public void Configure(EntityTypeBuilder<RoofAbuttingToTallerConstruction> builder)
        {
            builder.ToTable(SnowLoadsConstants.RoofsAbuttingToTallerConstructions, SnowLoadsConstants.SchemaName);

            builder.Property(a => a.WidthOfUpperBuilding)
                .IsRequired();
            builder.Property(a => a.WidthOfLowerBuilding)
                .IsRequired();
            builder.Property(a => a.HeightDifference)
                .IsRequired();
            builder.Property(a => a.SlopeOfHigherRoof)
                .IsRequired();
            builder.Property(a => a.SnowFencesOnHigherRoof)
                .IsRequired();

        }
    }
}
