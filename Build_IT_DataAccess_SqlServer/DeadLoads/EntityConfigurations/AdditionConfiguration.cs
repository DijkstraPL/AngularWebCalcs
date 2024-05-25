using Build_IT_DataAccess.DeadLoads;
using Build_IT_DataAccess.DeadLoads.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.EntityConfigurations
{
    public class AdditionConfiguration : IEntityTypeConfiguration<Addition>
    {
        public void Configure(EntityTypeBuilder<Addition> builder)
        {
            builder.ToTable(DeadLoadsConstants.Additions, DeadLoadsConstants.SchemaName);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(124);

            builder.Property(a => a.Value)
                .IsRequired();
        }
    }
}
