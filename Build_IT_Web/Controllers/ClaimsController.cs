using Build_IT_WebApplication.Claims.Commands;
using Build_IT_WebApplication.Claims.Queries;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{
    [Authorize]
    public class ClaimsController : ApiControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IMediator _mediator;

        public ClaimsController(IMediator mediator, ILogger<ClaimsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet( Name = "GetAllClaims")]
        public async Task<ActionResult<List<ClaimResource>>> GetAllClaims(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetClaimsQuery(), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the claims.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{projectId}", Name = "GetClaimsForProjectForCurrentUser")]
        public async Task<ActionResult<List<ClaimResource>>> GetClaimsForProjectForCurrentUser(int projectId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetClaimsForProjectForCurrentUserQuery(projectId), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the claims.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{userId}/{projectId}", Name = "GetClaimsForProjectForUser")]
        public async Task<ActionResult<List<ClaimResource>>> GetClaimsForProjectForUser(string userId, int projectId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetClaimsForProjectForUserQuery(userId, projectId), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the claims.");
            return Ok(result);
        }


        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{userId}/{projectId}/{claimName}", Name = "CheckIfUserHasClaimInProject")]
        public async Task<ActionResult<bool>> CheckIfUserHasClaimInProject(string userId, int projectId, int claimId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CheckIfUserHasClaimInProjectQuery(userId, projectId, claimId), cancellationToken);
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{userId}/{projectId}/{claimId}")]
        public async Task<ActionResult<bool>> AssignClaimToDesignerForProject(string userId, int projectId, int claimId, CancellationToken cancellationToken)
        {
            var assignClaimCommand = new AssignClaimCommand(userId, projectId, claimId);
            var result = await Mediator.Send(assignClaimCommand, cancellationToken);
            if (!result)
                return Problem("Something goes wrong when trying to assign a claim.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{userId}/{projectId}/{claimId}")]
        public async Task<ActionResult<bool>> RemoveClaimFromDesignerInProject(string userId, int projectId, int claimId, CancellationToken cancellationToken)
        {
            var removeClaimCommand = new RemoveClaimCommand(userId, projectId, claimId);
            var result = await Mediator.Send(removeClaimCommand, cancellationToken);
            if (!result)
                return Problem("Something goes wrong when trying to remove a claim.");
            return Ok(result);
        }
    }
}
