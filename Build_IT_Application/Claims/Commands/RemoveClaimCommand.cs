using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Claims.Commands
{
    [Authorize(Roles = "Administrator,Designer")]
    public class RemoveClaimCommand : IRequest<bool>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public int ClaimId { get; set; }

        public RemoveClaimCommand(string userId, int projectId, int claimId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));

            this.UserId = userId;
            this.ProjectId = projectId;
            ClaimId = claimId;
        }
    }

    public class RemoveClaimCommandHandler : IRequestHandler<RemoveClaimCommand, bool>
    {
        private readonly IProjectDesignerClaimRepository _projectDesignerClaimRepository;

        public RemoveClaimCommandHandler(IProjectDesignerClaimRepository projectDesignerClaimRepository)
        {
            _projectDesignerClaimRepository = projectDesignerClaimRepository ?? throw new ArgumentNullException(nameof(projectDesignerClaimRepository));
        }

        public async Task<bool> Handle(RemoveClaimCommand request, CancellationToken cancellationToken)
        {
            var projectDesignerClaims =  _projectDesignerClaimRepository.Find(pdc => pdc.ProjectId == request.ProjectId && pdc.ClaimId == request.ClaimId && pdc.DesignerId == request.UserId);

            _projectDesignerClaimRepository.RemoveRange(projectDesignerClaims);

            return true;
        }
    }
}
