using Build_IT_DataAccess.SteelProfiles.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.SteelProfiles.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles
{
    public class SteelProfilesUnitOfWork : ISteelProfilesUnitOfWork
    {
        public IProfileTypeRepository ProfileTypes { get; }

        private readonly ISteelProfilesDbContext _context;

        public SteelProfilesUnitOfWork(
            ISteelProfilesDbContext context,
            IProfileTypeRepository profileTypeRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ProfileTypes = profileTypeRepository ?? throw new ArgumentNullException(nameof(profileTypeRepository));
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
