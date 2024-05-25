using Build_IT_CommonTools.Interfaces;
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
    public class DeleteCompanyCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository,  IProjectsUnitOfWork projectsUnitOfWork)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
        }

        public async Task<int> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(request.Id, cancellationToken);

             _companyRepository.Remove(company);
            await _projectsUnitOfWork.CompleteAsync();

            return request.Id;
        }
    }
}
