using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public CompanyRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Company>> GetAllForUserAsync(string userId, CancellationToken cancellationToken = default)
        {
           return await ProjectsDbContext.Companies.Where(c => 
                ProjectsDbContext.UserCompanies.Where(uc => uc.UserId == userId).Any(uc => uc.CompanyId == c.Id))
                .ToListAsync();
        }
    }
}
