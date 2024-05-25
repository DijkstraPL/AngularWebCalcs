using AutoMapper;
using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryResultResource>>
    {
    }
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResultResource>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CategoryResultResource>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync()
                .Include(c => c.Subcategories)
                .ThenInclude(s => s.Materials)
                .ThenInclude(m => m.MaterialAdditions)
                .ToListAsync(cancellationToken);
            var categoriesResults = _mapper.Map<List<CategoryResultResource>>(categories);
            return categoriesResults;
        }
    }
}
