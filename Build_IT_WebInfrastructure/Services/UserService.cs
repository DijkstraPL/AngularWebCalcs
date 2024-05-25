using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using Build_IT_WebApplication.Common.Exceptions;
using Build_IT_WebApplication.Identity;
using Build_IT_WebApplication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebInfrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IProjectsDbContext _projectsDbContext;

        public UserService(IApplicationDbContext applicationDbContext, IProjectsDbContext projectsDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _projectsDbContext = projectsDbContext ?? throw new ArgumentNullException(nameof(projectsDbContext));
        }

        public async Task< Guid> GetUserIdByMail(string userMail)
        {
           var user = await  _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == userMail);
            if (user is null)
                throw new NotFoundException("Couldn't find a user with mail.");
            return Guid.Parse(user.Id);
        }

        public async Task<List<IdentityUser>> GetUsersAssignedToCompany(int companyId, CancellationToken cancellationToken)
        {
            var userCompanies = _projectsDbContext.UserCompanies.Where(uc => uc.CompanyId == companyId);
            var users = await _applicationDbContext.Users
                .Where(u => userCompanies.Any(uc => uc.UserId == u.Id ))
                .Select(u => u as IdentityUser)
                .ToListAsync();
           return users;
        }
    }
}
