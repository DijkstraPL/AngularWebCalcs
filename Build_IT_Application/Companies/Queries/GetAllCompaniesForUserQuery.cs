using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Companies.Queries
{

    [Authorize(Roles = "Administrator")]
    public class GetAllCompaniesForUserQuery : IRequest<List<CompanyResource>>
    {
        public string UserId { get; init; }
    }

    public class GetAllCompaniesForUserQueryHandler : IRequestHandler<GetAllCompaniesForUserQuery, List<CompanyResource>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesForUserQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CompanyResource>> Handle(GetAllCompaniesForUserQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllForUserAsync(request.UserId, cancellationToken);
            var companiesResults = _mapper.Map<List<CompanyResource>>(companies);
            return companiesResults;
        }
    }

    public class GetAllCompaniesForCurrentUserQuery : IRequest<List<CompanyResource>>
    {
    }

    public class GetAllCompaniesForCurrentUserQueryHandler : IRequestHandler<GetAllCompaniesForCurrentUserQuery, List<CompanyResource>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAllCompaniesForCurrentUserQueryHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CompanyResource>> Handle(GetAllCompaniesForCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllForUserAsync(_currentUserService.UserId, cancellationToken);
            var companiesResults = _mapper.Map<List<CompanyResource>>(companies);
            return companiesResults;
        }
    }
}
