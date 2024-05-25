using Build_IT_DataAccess.DeadLoads;
using Build_IT_DataAccess.DeadLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.EntityConfigurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable(DeadLoadsConstants.Materials, DeadLoadsConstants.SchemaName);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.MinimumDensity)
                .IsRequired();

            builder.Property(m => m.MaximumDensity)
                .IsRequired();

            builder.Property(m => m.Unit)
                .IsRequired();
        }
    }
}
