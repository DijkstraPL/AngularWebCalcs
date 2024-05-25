using AutoMapper;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllSubcategoriesForCategory
{
    public class GetAllSubcategoriesForCategoryQuery : IRequest<List<SubcategoryResultResource>>
    {
        public int CategoryId { get; set; }
    }
    public class GetAllSubcategoriesForCategoryQueryHandler : IRequestHandler<GetAllSubcategoriesForCategoryQuery, List<SubcategoryResultResource>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;

        public GetAllSubcategoriesForCategoryQueryHandler(ISubcategoryRepository  subcategoryRepository, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<SubcategoryResultResource>> Handle(GetAllSubcategoriesForCategoryQuery request, CancellationToken cancellationToken)
        {
            var subcategories = await _subcategoryRepository.GetAllSubcategoriesForCategoryAsync(request.CategoryId, cancellationToken);
            var subcategoriesResults = _mapper.Map<List<SubcategoryResultResource>>(subcategories);
            return subcategoriesResults;

            var t = new SubcategoryResultResource();
        }
    }
}
