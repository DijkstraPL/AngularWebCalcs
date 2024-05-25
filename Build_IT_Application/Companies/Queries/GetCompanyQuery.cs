using AutoMapper;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Mappings;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Companies.Queries
{

    [Authorize(Roles = "Administrator,Designer")]
    public class GetCompanyQuery : IRequest<CompanyResource>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyResource>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CompanyResource> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(request.CompanyId, cancellationToken);
            var companyResult = _mapper.Map<CompanyResource>(company);
            return companyResult;
        }
    }

}
