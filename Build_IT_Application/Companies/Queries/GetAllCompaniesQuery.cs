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

namespace Build_IT_WebApplication.Companies.Queries
{
    //[Authorize(Roles = "Administrator")]
    public class GetAllCompaniesQuery : IRequest<List<CompanyResource>>
    {
    }

    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyResource>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CompanyResource>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAll().ToListAsync(cancellationToken);
            var companiesResults = _mapper.Map<List<CompanyResource>>(companies);
            return companiesResults;
        }
    }
}
