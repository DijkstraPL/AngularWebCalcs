using Build_IT_WebInfrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Identity
{
    public interface IApplicationDbContext 
    {
        DbSet<ApplicationUser> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
