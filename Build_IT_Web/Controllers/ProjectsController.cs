using Build_IT_WebApplication.DeadLoads.Commands;
using Build_IT_WebApplication.DeadLoads.Queries;
using Build_IT_WebApplication.Projects.Commands;
using Build_IT_WebApplication.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{
    [Authorize]
    public sealed class ProjectsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetAllProjects")]
        public async Task<ActionResult<List<ProjectResource>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProjectsQuery(), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the projects.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{projectId}", Name = "GetProject")]
        public async Task<ActionResult<ProjectResource>> Get(int projectId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProjectQuery() { ProjectId = projectId}, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the project.");
            return Ok(result);
        }


        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("forCompany/{companyId}", Name = "GetAllProjectsForCompany")]
        public async Task<ActionResult<List<ProjectResource>>> GetAllProjectsForCompany(int companyId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProjectsForCompanyQuery { CompanyId = companyId }, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the projects.");
            return Ok(result);
        }


        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "CreateProject")]
        public async Task<ActionResult<int>> Create([FromBody] CreateProjectCommand createProjectCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createProjectCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to add new project.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(Name = "UpdateProject")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateProjectCommand updateProjectCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateProjectCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to update a project.");
            return Ok(result);
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{projectId}", Name = "DeleteProject")]
        public async Task<ActionResult<int>> Delete(int projectId, CancellationToken cancellationToken)
        {
            var deleteProjectCommand = new DeleteProjectCommand(projectId);
            await _mediator.Send(deleteProjectCommand, cancellationToken);
            return NoContent();
        }
    }
}
