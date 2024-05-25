using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.DeadLoads.Queries
{
    public class GetDeadLoadLayersQuery : IRequest<List<DeadLoadLayerResource>>
    {
        public int DeadLoadId { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
    }
    public class GetDeadLoadLayersQueryHandler : IRequestHandler<GetDeadLoadLayersQuery, List<DeadLoadLayerResource>>
    {
        private readonly IDeadLoadRepository _deadLoadRepository;
        private readonly IMapper _mapper;

        public GetDeadLoadLayersQueryHandler(IDeadLoadRepository deadLoadRepository, IMapper mapper)
        {
            _deadLoadRepository = deadLoadRepository ?? throw new ArgumentNullException(nameof(deadLoadRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<DeadLoadLayerResource>> Handle(GetDeadLoadLayersQuery request, CancellationToken cancellationToken)
        {
            var deadLoadLayers = await _deadLoadRepository.GetLayers(request.DeadLoadId, cancellationToken);
            var deadLoadLayersResults = _mapper.Map<List<DeadLoadLayerResource>>(deadLoadLayers);
            return deadLoadLayersResults;
        }
    }
}
