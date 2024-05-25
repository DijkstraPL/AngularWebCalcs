using System.Collections.Generic;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record BeamCalculationResultsResource(
            List<BeamCalculationResultResource> NormalForces,
            List<BeamCalculationResultResource> ShearForces,
            List<BeamCalculationResultResource> BendingMoments,
            List<BeamCalculationResultResource> HorizontalDeflections,
            List<BeamCalculationResultResource> VerticalDeflections,
            List<BeamCalculationResultResource> Rotations)
    {
    }
}
