using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Mappings;
using Build_IT_WebApplication.Common.Security;
using Build_IT_WebApplication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Companies.Queries
{
    [Authorize(Roles = "Administrator")]
    public class GetAllUsersForCompanyQuery : IRequest<List<UserResource>>
    {
        public int CompanyId { get; set; }
    }

    public class GetAllUsersForCompanyQueryHandler : IRequestHandler<GetAllUsersForCompanyQuery, List<UserResource>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllUsersForCompanyQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UserResource>> Handle(GetAllUsersForCompanyQuery request, CancellationToken cancellationToken)
        {
            var companies = await _userService.GetUsersAssignedToCompany(request.CompanyId, cancellationToken);
            var companiesResults = _mapper.Map<List<UserResource>>(companies);
            return companiesResults;
        }
    }
    public class UserResource : IMapFrom<IdentityUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
