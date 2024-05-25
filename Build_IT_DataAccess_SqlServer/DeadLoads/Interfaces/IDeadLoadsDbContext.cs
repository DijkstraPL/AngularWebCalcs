using Build_IT_DataAccess.DeadLoads.Entities;
using Microsoft.EntityFrameworkCore;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces
{
    public interface IDeadLoadsDbContext : IDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Subcategory> Subcategories { get; set; }
        DbSet<Material> Materials { get; set; }
        DbSet<Addition> Additions { get; set; }
        DbSet<MaterialAddition> MaterialAdditions { get; set; }
    }
}
