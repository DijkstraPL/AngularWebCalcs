using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess.SnowLoads.Repositories.Interfaces;
using Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Queries.GetAllSnowLoadsForProject
{

    public class GetAllSnowLoadsForProjectQuery : IRequest<List<SnowLoadResource>>
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
    }

    public class GetAllSnowLoadsForProjectQueryHandler : IRequestHandler<GetAllSnowLoadsForProjectQuery, List<SnowLoadResource>>
    {
        private readonly ISnowLoadRepository _snowLoadRepository;
        private readonly IMapper _mapper;

        public GetAllSnowLoadsForProjectQueryHandler(ISnowLoadRepository snowLoadRepository, IMapper mapper)
        {
            _snowLoadRepository = snowLoadRepository ?? throw new ArgumentNullException(nameof(snowLoadRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<SnowLoadResource>> Handle(GetAllSnowLoadsForProjectQuery request, CancellationToken cancellationToken)
        {
            var snowLoads = await _snowLoadRepository.GetAllSnowLoadsAsync(request.ProjectId, cancellationToken);
            var snowLoadsResults = _mapper.Map<List<SnowLoadResource>>(snowLoads);
            return snowLoadsResults;
        }
    }
}
