using AutoMapper;
using Build_IT_BeamStatica;
using Build_IT_BeamStatica.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    //[Authorize(Roles = "Administrator")]
    //[Authorize(Policy = "CanPurge")]
    public class CalculateBeamCommand : IRequest<BeamCalculationResultsResource>
    {
        public BeamResource BeamResource { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CalculateBeamCommand, BeamCalculationResultsResource>
    {
        private readonly IMapper _mapper;

        public CreateTodoListCommandHandler(IMapper mapper)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<BeamCalculationResultsResource> Handle(CalculateBeamCommand request, CancellationToken cancellationToken)
        {
            var beamData = _mapper.Map<BeamResource, BeamData>(request.BeamResource);
            var beamCalculator = new BeamCalculator(beamData);

            var results = await Task.Run(() => beamCalculator.Calculate(), cancellationToken);

            var calculationResults = new BeamCalculationResultsResource(
                results.NormalForces.Select(r => new BeamCalculationResultResource(r.Key, r.Value)).ToList(),
                results.ShearForces.Select(r => new BeamCalculationResultResource(r.Key, r.Value)).ToList(),
                results.BendingMoments.Select(r => new BeamCalculationResultResource(r.Key, r.Value)).ToList(),
                results.HorizontalDeflections.Select(r => new BeamCalculationResultResource(r.Key, r.Value)).ToList(),
                results.VerticalDeflections.Select(r => new BeamCalculationResultResource(r.Key, r.Value)).ToList(),
                results.Rotations.Select(r => new BeamCalculationResultResource(r.Key, r.Value)).ToList());

            return calculationResults;
        }
    }
}
