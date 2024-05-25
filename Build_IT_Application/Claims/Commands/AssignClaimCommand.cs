using AutoMapper;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Exceptions;
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
    public class AssignClaimCommand : IRequest<bool>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public int ClaimId { get; set; }

        public AssignClaimCommand(string userId, int projectId, int claimId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));

            this.UserId = userId;
            this.ProjectId = projectId;
            ClaimId = claimId;
        }
    }

    public class AssignClaimCommandHandler : IRequestHandler<AssignClaimCommand, bool>
    {
        private readonly IProjectDesignerClaimRepository _projectDesignerClaimRepository;

        public AssignClaimCommandHandler(IProjectDesignerClaimRepository projectDesignerClaimRepository)
        {
            _projectDesignerClaimRepository = projectDesignerClaimRepository ?? throw new ArgumentNullException(nameof(projectDesignerClaimRepository));
        }

        public async Task<bool> Handle(AssignClaimCommand request, CancellationToken cancellationToken)
        {
            var projectDesignerClaim = new ProjectDesignerClaim
            {
                ClaimId = request.ClaimId,
                ProjectId = request.ProjectId,
                DesignerId = request.UserId
            };

            await _projectDesignerClaimRepository.AddAsync(projectDesignerClaim, cancellationToken);

            return true;
        }
    }
}
