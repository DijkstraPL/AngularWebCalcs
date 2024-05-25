using Build_IT_DataAccess.DeadLoads;
using Build_IT_DataAccess.DeadLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.EntityConfigurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable(DeadLoadsConstants.Subcategories, DeadLoadsConstants.SchemaName);

            builder.Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany<Material>(sc => sc.Materials)
                .WithOne(m => m.Subcategory)
                .HasForeignKey(m => m.SubcategoryId);
        }
    }
}
