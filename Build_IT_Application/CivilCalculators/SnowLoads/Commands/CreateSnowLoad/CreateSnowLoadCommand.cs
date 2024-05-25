using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_DataAccess.SnowLoads.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Commands.CreateSnowLoad
{
    public class CreateSnowLoadCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public RoofType RoofType { get; set; }
    }

    public enum RoofType
    {
        None,
        Monopitch,
        Pitched,
        MultiSpan,
        Cylindrical,
        DriftingAtProjectionsObstructions,
        RoofAbuttingToTallerConstruction,
        Snowguards,
        SnowOverhanging
    }

    public class CreateSnowLoadCommandHandler : IRequestHandler<CreateSnowLoadCommand, int>
    {
        private readonly ISnowLoadRepository _snowLoadRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public CreateSnowLoadCommandHandler(ISnowLoadRepository snowLoadRepository, IProjectsUnitOfWork projectsUnitOfWork,
            IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _snowLoadRepository = snowLoadRepository ?? throw new ArgumentNullException(nameof(snowLoadRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<int> Handle(CreateSnowLoadCommand request, CancellationToken cancellationToken)
        {
            var entity = new SnowLoad
            {
                Name = request.Name,
                CreatedBy = _currentUserService.UserId,
                LastModifiedBy = _currentUserService.UserId,
                Created = _dateTime.Now,
                LastModified = _dateTime.Now,
            };

            await _snowLoadRepository.AddAsync(entity, cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            var projectEntityMapping = new ProjectSnowLoad
            {
                ProjectId = request.ProjectId,
                SnowLoadId = entity.Id,
            };

            switch (request.RoofType)
            {
                case RoofType.Monopitch:
                    var monopitchRoof = new MonopitchRoof
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(monopitchRoof);
                    break;
                case RoofType.Pitched:
                    var pitchedRoof = new PitchedRoof
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(pitchedRoof);
                    break;
                case RoofType.MultiSpan:
                    var multiSpanRoof = new MultiSpanRoof
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(multiSpanRoof);
                    break;
                case RoofType.Cylindrical:
                    var cylindricalRoof = new CylindricalRoof
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(cylindricalRoof);
                    break;
                case RoofType.DriftingAtProjectionsObstructions:
                    var driftingAtProjectionsObstructions = new DriftingAtProjectionsObstructions
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(driftingAtProjectionsObstructions);
                    break;
                case RoofType.RoofAbuttingToTallerConstruction:
                    var roofAbuttingToTallerConstruction = new RoofAbuttingToTallerConstruction
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(roofAbuttingToTallerConstruction);
                    break;
                case RoofType.Snowguards:
                    var snowguards = new Snowguards
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(snowguards);
                    break;
                case RoofType.SnowOverhanging:
                    var snowOverhanging = new SnowOverhanging
                    {
                        SnowLoadId = entity.Id
                    };
                    await _snowLoadRepository.AddRoof(snowOverhanging);
                    break;
                case RoofType.None:
                default:
                    break;
            }

            await _snowLoadRepository.AddProjectMapping(projectEntityMapping, cancellationToken);
            await _projectsUnitOfWork.CompleteAsync();

            return entity.Id;
        }
    }
}
