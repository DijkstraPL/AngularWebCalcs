using Build_IT_DataAccess.DeadLoads;
using Build_IT_DataAccess.DeadLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.EntityConfigurations
{
    public class MaterialAdditionConfiguration : IEntityTypeConfiguration<MaterialAddition>
    {
        public void Configure(EntityTypeBuilder<MaterialAddition> builder)
        {
            builder.ToTable(DeadLoadsConstants.MaterialAdditions, DeadLoadsConstants.SchemaName);

            builder.HasKey(ma
                => new { ma.MaterialId, ma.AdditionId });
        }
    }
}
