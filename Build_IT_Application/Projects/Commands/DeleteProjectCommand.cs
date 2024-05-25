using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
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

namespace Build_IT_WebApplication.Projects.Commands
{
    [Authorize(Roles = "Administrator,Designer")]
    public record DeleteProjectCommand(int Id) : IRequest;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IProjectsUnitOfWork projectsUnitOfWork)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.Id, cancellationToken);

            if (project is null)
                throw new NotFoundException(nameof(Project), request.Id);

            _projectRepository.Remove(project);

            await _projectsUnitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
