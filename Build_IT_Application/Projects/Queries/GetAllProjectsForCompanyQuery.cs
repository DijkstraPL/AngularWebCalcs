using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Projects.Queries
{
    [Authorize(Roles = "Administrator,Designer")]
    public class GetAllProjectsForCompanyQuery : IRequest<List<ProjectResource>>
    {
        public int CompanyId { get; set; }
    }

    public class GetAllProjectsForCompanyQueryHandler : IRequestHandler<GetAllProjectsForCompanyQuery, List<ProjectResource>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsForCompanyQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ProjectResource>> Handle(GetAllProjectsForCompanyQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllForCompanyAsync(request.CompanyId, cancellationToken);
            var projectsResults = _mapper.Map<List<ProjectResource>>(projects);
            return projectsResults;
        }
    }

}
