using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Interfaces;
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

namespace Build_IT_WebApplication.Projects.Commands
{
  [Authorize(Roles = "Administrator,Designer")]
    public class UpdateProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IProjectsUnitOfWork projectsUnitOfWork,
            IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.Id, cancellationToken);

            if (project is null)
                throw new ArgumentException("Project not found.");

            project.Name = request.Name;
            project.LastModified = _dateTime.Now;
            project.LastModifiedBy = _currentUserService.UserId;
            project.Description = request.Description;

            await _projectsUnitOfWork.CompleteAsync();

            return project.Id;
        }
    }
}
