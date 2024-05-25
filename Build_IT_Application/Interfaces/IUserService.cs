using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Interfaces
{
    public interface IUserService
    {
        Task<Guid> GetUserIdByMail(string userMail);
        Task<List<IdentityUser>> GetUsersAssignedToCompany(int companyId, CancellationToken cancellationToken);
    }
}
