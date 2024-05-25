using Build_IT_DataAccess.DeadLoads;
using Build_IT_DataAccess.DeadLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(DeadLoadsConstants.Categories, DeadLoadsConstants.SchemaName);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany<Subcategory>(c => c.Subcategories)
                .WithOne(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId);
        }
    }
}
