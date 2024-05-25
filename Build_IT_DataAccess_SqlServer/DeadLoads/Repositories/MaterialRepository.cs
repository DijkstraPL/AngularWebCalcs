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
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public IDeadLoadsDbContext DeadLoadsContext
            => Context as IDeadLoadsDbContext;

        public MaterialRepository(IDeadLoadsDbContext context)
            : base(context)
        {
        }
                
        public async Task<List<Material>> GetAllMaterialsForSubcategoryAsync(
            long subcategoryId, CancellationToken cancellationToken)
        {
            return await DeadLoadsContext.Materials
                .Where(m => m.Subcategory.Id == subcategoryId)
                .OrderBy(m => m.Name)
                .Include(m => m.MaterialAdditions)
                .ThenInclude(ma => ma.Addition)
                .ToListAsync(cancellationToken);
        }
    }
}
