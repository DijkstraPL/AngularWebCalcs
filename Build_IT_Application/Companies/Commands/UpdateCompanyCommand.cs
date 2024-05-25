using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Companies.Commands
{
    [Authorize(Roles = "Administrator,Designer")]
    public class UpdateCompanyCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService,
            IDateTime dateTime, IUserCompanyRepository userCompanyRepository, IProjectsUnitOfWork projectsUnitOfWork)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _userCompanyRepository = userCompanyRepository ?? throw new ArgumentNullException(nameof(userCompanyRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
        }

        public async Task<int> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company =   await _companyRepository.GetAsync(request.Id, cancellationToken);

            if (company is null)
                throw new ArgumentException("Company not found.");

            company.Name = request.Name;
            company.LastModified = _dateTime.Now;
            company.LastModifiedBy = _currentUserService.UserId;

            await _projectsUnitOfWork.CompleteAsync();

            return company.Id;
        }
    }
}
