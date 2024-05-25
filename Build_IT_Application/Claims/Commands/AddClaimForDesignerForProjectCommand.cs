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
    public class AddClaimForDesignerForProjectCommand : IRequest<bool>
    {
        public int ProjectId { get; set; }
        public string ClaimName { get; set; }
        public string UserId { get; set; }
    }

    public class AddClaimForDesignerForProjectCommandHandler : IRequestHandler<AddClaimForDesignerForProjectCommand, bool>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IProjectDesignerClaimRepository _projectDesignerClaimRepository;
        private readonly IMapper _mapper;

        public AddClaimForDesignerForProjectCommandHandler(IClaimRepository claimRepository,
            IProjectDesignerClaimRepository projectDesignerClaimRepository,
            IMapper mapper)
        {
            _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
            _projectDesignerClaimRepository = projectDesignerClaimRepository ?? throw new ArgumentNullException(nameof(projectDesignerClaimRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(AddClaimForDesignerForProjectCommand request, CancellationToken cancellationToken)
        {
            var claim = await _claimRepository.FindAsync(request.ClaimName, cancellationToken);
            if (claim is null)
                throw new NotFoundException("Couldn't find claim with name " + request.ClaimName);

            var projectDesignerClaim = new ProjectDesignerClaim
            {
                ClaimId = claim.Id,
                ProjectId = request.ProjectId,
                DesignerId = request.UserId
            };
            await _projectDesignerClaimRepository.AddAsync(projectDesignerClaim, cancellationToken);

            return true;
        }
    }
}
