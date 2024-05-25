using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Companies.Commands;
using Build_IT_WebApplication.Companies.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{
//    [Authorize]
    public class CompaniesController : ApiControllerBase
    {
        private readonly IDataCache _dataCache;

        public CompaniesController(IDataCache dataCache) 
        {
            _dataCache = dataCache ?? throw new ArgumentNullException(nameof(dataCache));
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public async Task<ActionResult<List<CompanyResource>>> GetAll(CancellationToken cancellationToken)
        {
            const string companies = "companies";

            var cachedCompanies = await _dataCache.GetCacheData<List<CompanyResource>>(companies);
            if (cachedCompanies is not null)
                return Ok(cachedCompanies);

            var result = await Mediator.Send(new GetAllCompaniesQuery(), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the companies.");

            await _dataCache.SetCacheData<List<CompanyResource>>(companies, result, TimeSpan.FromMinutes(5));

            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("forUser/{userId}")]
        public async Task<ActionResult<List<CompanyResource>>> GetAllForUser(string userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllCompaniesForUserQuery { UserId = userId }, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the companies.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("currentUser")]
        public async Task<ActionResult<List<CompanyResource>>> GetAllForCurrentUser(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllCompaniesForCurrentUserQuery(), cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the companies.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyResource>> Get(int companyId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await Mediator.Send(new GetCompanyQuery { CompanyId = companyId }, cancellationToken);
                if (result is null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Something goes wrong when trying to get the companies.");
            }
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost()]
        public async Task<ActionResult<int>> Create([FromBody] CreateCompanyCommand createCompanyCommand, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(createCompanyCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to add new company.");
            return Ok(result);
        }
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut()]
        public async Task<ActionResult<int>> Update([FromBody] UpdateCompanyCommand updateCompanyCommand, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateCompanyCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to update a company.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{companyId}")]
        public async Task<ActionResult<int>> Delete(int companyId, CancellationToken cancellationToken)
        {
            var deleteCompanyCommand = new DeleteCompanyCommand
            {
                Id = companyId
            };
            var result = await Mediator.Send(deleteCompanyCommand, cancellationToken);
            if (result == 0)
                return Problem("Something goes wrong when trying to delete a company.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{companyId}/{userMail}")]
        public async Task<ActionResult<bool>> AssignUser(int companyId, string userMail, CancellationToken cancellationToken)
        {
            var assignUserCommand = new AssignUserCommand
            {
                CompanyId = companyId,
                UserMail = userMail
            };
            var result = await Mediator.Send(assignUserCommand, cancellationToken);
            if (!result)
                return Problem("Something goes wrong when trying to assign a user to a company.");
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{companyId}/users")]
        public async Task<ActionResult<List<UserResource>>> GetUsersForCompany(int companyId, CancellationToken cancellationToken)
        {
            var getAllUsersForCompanyQuery = new GetAllUsersForCompanyQuery
            {
                CompanyId = companyId
            };
            var result = await Mediator.Send(getAllUsersForCompanyQuery, cancellationToken);
            if (result is null)
                return Problem("Something goes wrong when trying to get the users.");
            return Ok(result);
        }
    }
}
