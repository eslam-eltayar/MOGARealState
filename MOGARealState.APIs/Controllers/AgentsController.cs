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
                return BadRequest(new { Message = ex.Message });
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
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("Orders/{agentId}")]
        public async Task<ActionResult<IReadOnlyList<UserOrderResponse>>> GetOrders(int agentId, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _agentService.GetOrdersAsync(agentId, cancellationToken);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("ChangeOrderStatus/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, [FromBody] ChangeOrderStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _agentService.ChangeOrderStatusAsync(orderId, request.NewStatus, cancellationToken);

                return Ok(new { Message = "Order Status Changed Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}