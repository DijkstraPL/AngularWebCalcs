using Build_IT_CommonTools.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.DeadLoads.Commands
{
    public class SaveDeadLoadLayersCommand : IRequest
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DeadLoadId { get; set; }
        public List<SaveDeadLoadLayer> DeadLoadLayers { get; set; }
    }

    public class SaveDeadLoadLayer
    {
        public int MaterialId { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public int? PreviousDeadLoadId { get; set; }
    }

    public class SaveDeadLoadLayersCommandHandler : IRequestHandler<SaveDeadLoadLayersCommand>
    {
        private readonly IDeadLoadRepository _deadLoadRepository;
        private readonly IProjectsUnitOfWork _projectsUnitOfWork;

        public SaveDeadLoadLayersCommandHandler(IDeadLoadRepository deadLoadRepository, IProjectsUnitOfWork projectsUnitOfWork,
            IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _deadLoadRepository = deadLoadRepository ?? throw new ArgumentNullException(nameof(deadLoadRepository));
            _projectsUnitOfWork = projectsUnitOfWork ?? throw new ArgumentNullException(nameof(projectsUnitOfWork));
        }

        public async Task<Unit> Handle(SaveDeadLoadLayersCommand request, CancellationToken cancellationToken)
        {
            _deadLoadRepository.RemoveLayers(request.DeadLoadId);

            var deadLoadLayers = new List<DeadLoadLayer>();
            int? previousId = null;

            foreach (var dll in request.DeadLoadLayers)
            {
                var deadLoadLayer = new DeadLoadLayer
                {
                    DeadLoadId = request.DeadLoadId,
                    Height = dll.Height,
                    Width = dll.Width,
                    Length = dll.Length,
                    MaterialId = dll.MaterialId,
                    PreviousDeadLoadId = previousId
                };

                await _deadLoadRepository.AddLayerAsync(deadLoadLayer, cancellationToken);
                await _projectsUnitOfWork.CompleteAsync();
                previousId = deadLoadLayer.Id;
            }

            return Unit.Value;
        }
    }
}
