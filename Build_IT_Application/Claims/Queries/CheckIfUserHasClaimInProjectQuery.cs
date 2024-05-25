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
        public class CheckIfUserHasClaimInProjectQuery : IRequest<bool>
    {
        public int ProjectId { get; set; }
        public int ClaimId { get; }
        public string UserId { get; set; }

        public CheckIfUserHasClaimInProjectQuery(string userId, int projectId, int claimId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));

            UserId = userId;
            ProjectId = projectId;
            ClaimId = claimId;
        }
    }

        public class CheckIfUserHasClaimInProjectQueryHandler : IRequestHandler<CheckIfUserHasClaimInProjectQuery, bool>
    {
        private readonly IClaimRepository _claimRepository;

        public CheckIfUserHasClaimInProjectQueryHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
        }
        public async Task<bool> Handle(CheckIfUserHasClaimInProjectQuery request, CancellationToken cancellationToken)
        {
            var claims = await _claimRepository.GetClaimsForUserAndProject(request.UserId, request.ProjectId, cancellationToken);
            return claims.Any(c => c.Id == request.ClaimId);
        }
    }
}
