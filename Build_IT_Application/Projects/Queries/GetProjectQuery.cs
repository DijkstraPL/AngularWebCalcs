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

namespace Build_IT_WebApplication.Projects.Queries
{
    [Authorize(Roles = "Administrator,Designer")]
    public class GetProjectQuery : IRequest<ProjectResource>
    {
        public int ProjectId { get; set; }
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectResource>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProjectResource> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.ProjectId, cancellationToken);
            var projectResult = _mapper.Map<ProjectResource>(project);
            return projectResult;
        }
    }
}
