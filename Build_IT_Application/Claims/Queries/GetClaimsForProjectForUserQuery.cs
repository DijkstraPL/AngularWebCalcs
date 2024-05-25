using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
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
    public class GetClaimsForProjectForUserQuery : IRequest<List<ClaimResource>>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }

        public GetClaimsForProjectForUserQuery(string userId, int projectId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));

            UserId = userId;
            ProjectId = projectId;
        }
    }

    public class GetClaimsForProjectForUserQueryHandler : IRequestHandler<GetClaimsForProjectForUserQuery, List<ClaimResource>>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IProjectDesignerClaimRepository _projectDesignerClaimRepository;
        private readonly IMapper _mapper;

        public GetClaimsForProjectForUserQueryHandler(IClaimRepository claimRepository,
            IProjectDesignerClaimRepository projectDesignerClaimRepository,
            IMapper mapper)
        {
            _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
            _projectDesignerClaimRepository = projectDesignerClaimRepository ?? throw new ArgumentNullException(nameof(projectDesignerClaimRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ClaimResource>> Handle(GetClaimsForProjectForUserQuery request, CancellationToken cancellationToken)
        {
            var claims = await _claimRepository.GetClaimsForUserAndProject(request.UserId, request.ProjectId, cancellationToken);
            var claimsResults = _mapper.Map<List<ClaimResource>>(claims);
            return claimsResults;
        }
    }
}
