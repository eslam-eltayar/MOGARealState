using MOGARealState.Core.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Services
{
    public interface IAgentService
    {
        Task<AgentResponse> AddAgentAsync(AddAgentRequest request, CancellationToken cancellationToken);
        Task<IReadOnlyList<AgentResponse>> GetAllAgentsAsync(CancellationToken cancellationToken);
    }
}
