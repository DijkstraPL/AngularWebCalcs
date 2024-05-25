using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Projects.Queries
{
    [Authorize(Roles = "Administrator,Designer")]
    public class GetAllProjectsQuery : IRequest<List<ProjectResource>>
    {
    }

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectResource>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ProjectResource>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAll().ToListAsync(cancellationToken);
            var projectsResults = _mapper.Map<List<ProjectResource>>(projects);
            return projectsResults;
        }
    }

}
