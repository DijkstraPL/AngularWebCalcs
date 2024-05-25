using Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Build_IT_Web.Controllers
{
    [Authorize]
    public class BeamCalculatorController : ApiControllerBase
    {
        private readonly ILogger<BeamCalculatorController> _logger;

        public BeamCalculatorController(ILogger<BeamCalculatorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Calculates the beam
        /// </summary>
        /// <param name="command">Beam data</param>
        /// <returns>Results</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /BeamCalculator
        ///{
        ///  "beamResource": {
        ///    "includeSelfWeight": false,
        ///    "spanDatas": [
        ///      {
        ///        "length": 10,
        ///        "material": {
        ///          "youngModulus": 30,
        ///          "density": 2400,
        ///          "thermalExpansionCoefficient": 0.000010
        ///        },
        ///        "section": {
        ///          "momentOfInteria": 312500,
        ///          "area": 1500,
        ///          "circumference": 160,
        ///          "solidHeight": 50
        ///        },
        ///        "leftNode": {
        ///    "angle": 0,
        ///          "nodeType": 0,
        ///          "pointLoads": [
        ///            {
        ///        "position": 0,
        ///              "value": 0,
        ///              "angle": 0,
        ///              "pointLoadType": 0
        ///            }
        ///          ]
        ///        },
        ///        "rightNode": {
        ///    "angle": 0,
        ///          "nodeType": 0,
        ///          "pointLoads": [
        ///            {
        ///        "position": 0,
        ///              "value": 0,
        ///              "angle": 0,
        ///              "pointLoadType": 0
        ///            }
        ///          ]
        ///        },
        ///        "includeSelfWeight": false,
        ///        "continuousLoads": [
        ///          {
        ///            "startPosition": 0,
        ///            "endPosition": 10,
        ///            "startValue": 50,
        ///            "endValue": 50,
        ///            "angle": 0,
        ///            "continuousLoadType": 2
        ///          }
        ///        ],
        ///        "pointLoads": [
        ///          {
        ///            "position": 0,
        ///            "value": 0,
        ///            "angle": 0,
        ///            "pointLoadType": 0
        ///          }
        ///        ]
        ///      }
        ///    ]
        ///  }
        ///}
        ///</remarks>
        /// <response code="201">Returns the calculations</response>
        /// <response code="400">If the input is invalid</response>         
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<BeamCalculationResultsResource>> CalculateBeamAsync([FromBody] CalculateBeamCommand beamData)
        {
            var result = await Mediator.Send(beamData);
            if (result is null)
                return Problem("Something goes wrong during caclulation.");
            return Ok(result);
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class FrameCalculatorController : ApiControllerBase
    {
        private readonly ILogger<FrameCalculatorController> _logger;

        public FrameCalculatorController(ILogger<FrameCalculatorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Calculates the frame
        /// </summary>
        /// <param name="command">Frame data</param>
        /// <returns>Results</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /FrameCalculator
        ///{
        ///
        ///}
        ///</remarks>
        /// <response code="201">Returns the calculations</response>
        /// <response code="400">If the input is invalid</response>         
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[HttpPost]
        //public async Task<ActionResult<BeamCalculationResultsResource>> CalculateFrameAsync([FromBody] CalculateFrameCommand frameData)
        //{
        //    var result = await _mediator.Send(frameData);
        //    if (result is null)
        //        return Problem("Something goes wrong during caclulation.");
        //    return Ok(result);
        //}
    }
}
