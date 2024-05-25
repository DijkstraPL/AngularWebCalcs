using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.DeadLoads.Queries
{

    public class GetDeadLoadsForProjectQuery : IRequest<List<DeadLoadResource>>
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
    }

    public class GetDeadLoadsForProjectQueryHandler : IRequestHandler<GetDeadLoadsForProjectQuery, List<DeadLoadResource>>
    {
        private readonly IDeadLoadRepository _deadLoadRepository;
        private readonly IMapper _mapper;

        public GetDeadLoadsForProjectQueryHandler(IDeadLoadRepository deadLoadRepository, IMapper mapper)
        {
            _deadLoadRepository = deadLoadRepository ?? throw new ArgumentNullException(nameof(deadLoadRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<DeadLoadResource>> Handle(GetDeadLoadsForProjectQuery request, CancellationToken cancellationToken)
        {
            var deadLoads = await _deadLoadRepository.GetAllForProjectAsync(request.ProjectId, cancellationToken);
            var deadLoadsResults = _mapper.Map<List<DeadLoadResource>>(deadLoads);
            return deadLoadsResults;
        }
    }
}
