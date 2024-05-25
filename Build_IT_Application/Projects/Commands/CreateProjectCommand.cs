using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Common.Security;
using Build_IT_WebDomain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Projects.Commands
{
    [Authorize(Roles = "Administrator,Designer")]
    public class CreateProjectCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IProjectsUnitOfWork projectsUnitOfWork, 
            IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = new Project
            {
                CompanyId= request.CompanyId,
                Name= request.Name,
                Description= request.Description,
                Created= _dateTime.Now,
                LastModified = _dateTime.Now,
                CreatedBy = _currentUserService.UserId,
                LastModifiedBy = _currentUserService.UserId,
            };

            entity.AddDomainEvent(new ProjectCreatedEvent(entity));

            await _projectRepository.AddAsync(entity , cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            return entity.Id;
        }
    }
}
