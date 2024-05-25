using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Claims.Queries
{
    [Authorize(Roles = "Administrator,Designer")]
    public class GetClaimsForProjectForCurrentUserQuery : IRequest<List<ClaimResource>>
    {
        public int ProjectId { get; set; }

        public GetClaimsForProjectForCurrentUserQuery(int projectId)
        {
            ProjectId = projectId;
        }
    }

    public class GetClaimsForProjectForCurrentUserQueryHandler : IRequestHandler<GetClaimsForProjectForCurrentUserQuery, List<ClaimResource>>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IProjectDesignerClaimRepository _projectDesignerClaimRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetClaimsForProjectForCurrentUserQueryHandler(IClaimRepository claimRepository,
            IProjectDesignerClaimRepository projectDesignerClaimRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
            _projectDesignerClaimRepository = projectDesignerClaimRepository ?? throw new ArgumentNullException(nameof(projectDesignerClaimRepository));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ClaimResource>> Handle(GetClaimsForProjectForCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var claims = await _claimRepository.GetClaimsForUserAndProject(_currentUserService.UserId, request.ProjectId, cancellationToken);
            var claimsResults = _mapper.Map<List<ClaimResource>>(claims);
            return claimsResults;
        }
    }
}
