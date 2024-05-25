using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries;
using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllCategories;
using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllMaterialsForSubcategory;
using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllSubcategoriesForCategory;
using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetMaterial;
using Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam;
using Build_IT_WebApplication.DeadLoads.Commands;
using Build_IT_WebApplication.DeadLoads.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{
    [Authorize]
    public class DeadLoadsController : ApiControllerBase
    {
        private readonly ILogger<DeadLoadsController> _logger;
        private readonly IMediator _mediator;

        public DeadLoadsController(IMediator mediator, ILogger<DeadLoadsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{companyId}/{projectId}/", Name = "GetAllDeadLoadsForProject")]
        public async Task<ActionResult<List<DeadLoadResource>>> GetAllDeadLoadsForProject(int companyId, int projectId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeadLoadsForProjectQuery { CompanyId = companyId, ProjectId = projectId }, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the dead loads.");
            return Ok(result);
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{companyId}/{projectId}/{deadLoadId}/layers", Name = "GetAllDeadLoadLayers")]
        public async Task<ActionResult<List<DeadLoadLayerResource>>> GetAllDeadLoadLayers(int companyId, int projectId, int deadLoadId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeadLoadLayersQuery { CompanyId = companyId, ProjectId = projectId , DeadLoadId = deadLoadId }, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the dead load layers.");
            return Ok(result);
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{companyId}/{projectId}/create")]
        public async Task<ActionResult<int>> Create(int companyId, int projectId, [FromBody] CreateDeadLoadCommand createDeadLoadCommand, CancellationToken cancellationToken)
        {
            createDeadLoadCommand.CompanyId = companyId;
            createDeadLoadCommand.ProjectId = projectId;

            var result = await _mediator.Send(createDeadLoadCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to add new dead load.");
            return Ok(result);
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{companyId}/{projectId}/{deadLoadId}/layers/create")]
        public async Task<ActionResult<int>> SaveDeadLoadLayers(int companyId, int projectId, int deadLoadId ,[FromBody] SaveDeadLoadLayersCommand saveDeadLoadLayersCommand, CancellationToken cancellationToken)
        {
            saveDeadLoadLayersCommand.CompanyId = companyId;
            saveDeadLoadLayersCommand.ProjectId = projectId;
            saveDeadLoadLayersCommand.DeadLoadId = deadLoadId;

           await _mediator.Send(saveDeadLoadLayersCommand, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>Results</returns>
        /// <response code="200">Returns the categories</response>
        /// <response code="400">If the input is invalid</response>         
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryResultResource>>> GetAllCategories( CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the categories for dead loads.");
            return Ok(result);
        }

        /// <summary>
        /// Get all subcategories 
        /// </summary>
        /// <returns>Results</returns>
        /// <response code="200">Returns the categories</response>
        /// <response code="400">If the input is invalid</response>         
        /// <response code="404">If the result is not found</response>         
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{categoryId}/subcategories")]
        public async Task<ActionResult<List<SubcategoryResultResource>>> GetAllSubcategories(int categoryId, CancellationToken cancellationToken)
        {
            var getAllSubcategoriesForCategoryQuery = new GetAllSubcategoriesForCategoryQuery { CategoryId = categoryId };
            var result = await Mediator.Send(getAllSubcategoriesForCategoryQuery, cancellationToken);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all materials 
        /// </summary>
        /// <returns>Results</returns>
        /// <response code="200">Returns the materials</response>
        /// <response code="400">If the input is invalid</response>         
        /// <response code="404">If the result is not found</response>         
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{subcategoryId}/materials")]
        public async Task<ActionResult<List<MaterialResultResource>>> GetAllMaterials(int subcategoryId, CancellationToken cancellationToken)
        {
            var getAllMaterialsForCategoryQuery = new GetAllMaterialsForSubcategoryQuery { SubcategoryId = subcategoryId };
            var result = await Mediator.Send(getAllMaterialsForCategoryQuery, cancellationToken);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Get material 
        /// </summary>
        /// <returns>Results</returns>
        /// <response code="200">Returns the material</response>
        /// <response code="400">If the input is invalid</response>         
        /// <response code="404">If the result is not found</response>         
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("materials/{materialId}")]
        public async Task<ActionResult<MaterialResultResource>> GetMaterial(int materialId, CancellationToken cancellationToken)
        {
            var getMaterialQuery = new GetMaterialQuery { MaterialId = materialId };
            var result = await Mediator.Send(getMaterialQuery, cancellationToken);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

    }
}
