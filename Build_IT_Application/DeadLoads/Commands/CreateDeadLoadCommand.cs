using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebDomain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.DeadLoads.Commands
{
    public class CreateDeadLoadCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }

    public class CreateDeadLoadCommandHandler : IRequestHandler<CreateDeadLoadCommand, int>
    {
        private readonly IDeadLoadRepository _deadLoadRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public CreateDeadLoadCommandHandler(IDeadLoadRepository  deadLoadRepository, IProjectsUnitOfWork projectsUnitOfWork,
            IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _deadLoadRepository = deadLoadRepository ?? throw new ArgumentNullException(nameof(deadLoadRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<int> Handle(CreateDeadLoadCommand request, CancellationToken cancellationToken)
        {
            var entity = new DeadLoad
            {
                Name = request.Name,
                CreatedBy = _currentUserService.UserId,
                LastModifiedBy = _currentUserService.UserId,
                Created = _dateTime.Now,
                LastModified = _dateTime.Now,
            };

            entity.AddDomainEvent(new DeadLoadCreatedEvent(entity));

            await _deadLoadRepository.AddAsync(entity , cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            var projectEntityMapping = new ProjectDeadLoad
            {
                ProjectId = request.ProjectId,
                DeadLoadId = entity.Id,
            };

            await _deadLoadRepository.AddProjectMapping(projectEntityMapping, cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            return entity.Id;
        }
    }
}
