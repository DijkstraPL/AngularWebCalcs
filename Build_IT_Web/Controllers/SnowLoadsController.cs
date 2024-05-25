using Build_IT_WebApplication.CivilCalculators.SnowLoads.Commands.CreateSnowLoad;
using Build_IT_WebApplication.CivilCalculators.SnowLoads.Queries.GetAllSnowLoadsForProject;
using Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{

        [Authorize]
        public class SnowLoadsController : ApiControllerBase
        {
            private readonly ILogger<SnowLoadsController> _logger;
            private readonly IMediator _mediator;

            public SnowLoadsController(IMediator mediator, ILogger<SnowLoadsController> logger)
            {
                _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
                _logger = logger;
            }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{companyId}/{projectId}", Name = "GetAllSnowLoadsForProject")]
        public async Task<ActionResult<List<SnowLoadResource>>> GetAllSnowLoadsForProject(int companyId, int projectId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSnowLoadsForProjectQuery { CompanyId = companyId, ProjectId = projectId }, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the snow loads.");
            return Ok(result);
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{companyId}/{projectId}/create")]
        public async Task<ActionResult<int>> Create(int companyId, int projectId, [FromBody] CreateSnowLoadCommand createSnowLoadCommand, CancellationToken cancellationToken)
        {
            createSnowLoadCommand.CompanyId = companyId;
            createSnowLoadCommand.ProjectId = projectId;

            var result = await _mediator.Send(createSnowLoadCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to add new snow load.");
            return Ok(result);
        }

    }
}
