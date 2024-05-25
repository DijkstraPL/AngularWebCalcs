using AutoMapper;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetMaterial
{
    public class GetMaterialQuery : IRequest<MaterialResultResource>
    {
        public int MaterialId { get; set; }
    }
    public class GetMaterialQueryHandler : IRequestHandler<GetMaterialQuery, MaterialResultResource>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MaterialResultResource> Handle(GetMaterialQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetAsync(request.MaterialId, cancellationToken);
            var materialResult = _mapper.Map<MaterialResultResource>(material);
            return materialResult;
        }
    }
}
