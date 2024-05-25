using AutoMapper;
using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllMaterialsForSubcategory
{
    public class GetAllMaterialsForSubcategoryQuery : IRequest<List<MaterialResultResource>>
    {
        public int SubcategoryId { get; set; }
    }
    public class GetAllMaterialsForSubcategoryQueryHandler : IRequestHandler<GetAllMaterialsForSubcategoryQuery, List<MaterialResultResource>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetAllMaterialsForSubcategoryQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<MaterialResultResource>> Handle(GetAllMaterialsForSubcategoryQuery request, CancellationToken cancellationToken)
        {
            var materials = await _materialRepository.GetAllMaterialsForSubcategoryAsync(request.SubcategoryId, cancellationToken);
            var materialResults = _mapper.Map<List<MaterialResultResource>>(materials);
            return materialResults;
        }
    }

    public class MaterialAdditionResult : IMapFrom<Addition>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
    }
}
