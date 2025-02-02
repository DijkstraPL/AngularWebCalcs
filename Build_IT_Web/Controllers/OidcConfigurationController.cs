﻿using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OidcConfigurationController : Controller
    {
        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }
        private readonly ILogger<OidcConfigurationController> _logger;

        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider, ILogger<OidcConfigurationController> logger)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider ?? throw new ArgumentNullException(nameof(clientRequestParametersProvider));
            _logger = logger;
        }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
