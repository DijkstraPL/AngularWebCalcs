using Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces;
using Build_IT_DataAccess.DeadLoads.Repositories;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.DeadLoads
{
    public sealed class DeadLoadsUnitOfWork : IDeadLoadsUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        public ISubcategoryRepository Subcategories { get; }
        public IMaterialRepository Materials { get; }

        private readonly IDeadLoadsDbContext _context;

        public DeadLoadsUnitOfWork(
            IDeadLoadsDbContext context,
            ICategoryRepository categoryRepository,
            ISubcategoryRepository subcategoryRepository,
            IMaterialRepository materialRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
            Categories = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            Subcategories = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
            Materials = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync()
        {
            return await CompleteAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
