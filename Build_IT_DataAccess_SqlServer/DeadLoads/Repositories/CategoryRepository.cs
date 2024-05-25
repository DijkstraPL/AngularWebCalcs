using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public IDeadLoadsDbContext DeadLoadsContext
             => Context as IDeadLoadsDbContext;
                
        public CategoryRepository(IDeadLoadsDbContext context)
             : base(context)
        {
        }

        public IQueryable<Category> GetAllCategoriesAsync()
        {
           return  DeadLoadsContext.Categories;
        }
    }
}
