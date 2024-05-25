using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        public IDeadLoadsDbContext DeadLoadsContext
            => Context as IDeadLoadsDbContext;

        public SubcategoryRepository(IDeadLoadsDbContext context)
            : base(context)
        {
        }

        public async Task<List<Subcategory>> GetAllSubcategoriesForCategoryAsync(long categoryId, CancellationToken cancellationToken)
        {
            return await DeadLoadsContext.Subcategories
                .Where(sc => sc.Category.Id == categoryId)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
