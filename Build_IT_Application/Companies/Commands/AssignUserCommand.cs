using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Common.Security;
using Build_IT_WebApplication.Interfaces;
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
    public class AssignUserCommand : IRequest<bool>
    {
        public int CompanyId { get; set; }
        public string UserMail { get; set; }
    }

    public class AssignUserCommandHandler : IRequestHandler<AssignUserCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IDesignerRepository _designerRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;

        public AssignUserCommandHandler(IUserService userService,
            IDesignerRepository designerRepository,
            IUserCompanyRepository userCompanyRepository, IProjectsUnitOfWork projectsUnitOfWork)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _designerRepository = designerRepository ?? throw new ArgumentNullException(nameof(designerRepository));
            _userCompanyRepository = userCompanyRepository ?? throw new ArgumentNullException(nameof(userCompanyRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
        }

        public async Task<bool> Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetUserIdByMail(request.UserMail);

            var companyUser = new UserCompany
            {
                CompanyId= request.CompanyId,
                UserId = userId.ToString()
            };

            await _userCompanyRepository.AddAsync(companyUser, cancellationToken);
            await _designerRepository.AddAsync(new Designer { UserId = userId.ToString() });
            await _projectsUnitOfWork.CompleteAsync();

            return true;
        }
    }
}
