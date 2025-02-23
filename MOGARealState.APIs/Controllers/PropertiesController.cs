using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGARealState.Core.DTOs.Requests;
using MOGARealState.Core.DTOs.Responses;
using MOGARealState.Core.Services;

namespace MOGARealState.APIs.Controllers
{
    public class PropertiesController(IPropertyService propertyService) : ApiBaseController
    {
        private readonly IPropertyService _propertyService = propertyService;

        [HttpPost("")]
        public async Task<IActionResult> AddProperty([FromForm] AddPropertyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _propertyService.AddPropertyAsync(request, cancellationToken);

                return Ok(property);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProperties(CancellationToken cancellationToken)
        {
            try
            {
                var properties = await _propertyService.GetPropertiesAsync(cancellationToken);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _propertyService.GetPropertyByIdAsync(id, cancellationToken);
                return Ok(property);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByAgent")]
        public async Task<ActionResult<IReadOnlyList<PropertyResponse>>> GetAgentProperties([FromQuery] int agentId, CancellationToken cancellationToken)
        {
            try
            {
                var props = await _propertyService.GetAgentPropertiesAsync(agentId, cancellationToken);

                return Ok(props);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromForm] AddPropertyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _propertyService.UpdatePropertyAsync(id, request, cancellationToken);
                return Ok(property);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("MakePropertySold")]
        public async Task<IActionResult> MakePropertySold([FromQuery] int propertyId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _propertyService.MakePropertySoldAsync(propertyId, cancellationToken);

                return Ok(new { Message = "Property Status Changed Successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}