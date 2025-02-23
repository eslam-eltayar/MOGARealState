using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGARealState.Core.DTOs.Requests;
using MOGARealState.Core.DTOs.Responses;
using MOGARealState.Core.Services;

namespace MOGARealState.APIs.Controllers
{
    public class AgentsController(IAgentService agentService) : ApiBaseController
    {
        private readonly IAgentService _agentService = agentService;

        [HttpPost("")]
        public async Task<IActionResult> AddAgent([FromForm] AddAgentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var agent = await _agentService.AddAgentAsync(request, cancellationToken);
                return Ok(agent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<AgentResponse>>> GetAllAgents(CancellationToken cancellationToken)
        {
            try
            {
                var agents = await _agentService.GetAllAgentsAsync(cancellationToken);

                return Ok(agents);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}