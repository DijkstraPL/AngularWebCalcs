using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Companies.Commands
{
    [Authorize(Roles = "Administrator,Designer")]
    public class CreateCompanyCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService,
            IDateTime dateTime, IUserCompanyRepository userCompanyRepository, IProjectsUnitOfWork projectsUnitOfWork)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _userCompanyRepository = userCompanyRepository ?? throw new ArgumentNullException(nameof(userCompanyRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = new Company
            {
                Name= request.Name,
                Created = _dateTime.Now,
                LastModified = _dateTime.Now,
                CreatedBy = _currentUserService.UserId,
                LastModifiedBy = _currentUserService.UserId
            };

            await _companyRepository.AddAsync(companyEntity , cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            var companyUser = new UserCompany { CompanyId = companyEntity.Id, UserId = _currentUserService.UserId };
            await _userCompanyRepository.AddAsync(companyUser, cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            return companyEntity.Id;
        }
    }
}
