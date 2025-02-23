using Microsoft.AspNetCore.Identity;
using MOGARealState.Core.Entities;
using MOGARealState.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Services
{
    public class AgentService(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        UserManager<AppUser> userManager) : IAgentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<AgentResponse> AddAgentAsync(AddAgentRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. The body cannot be empty");
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            var agentUser = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone,
                City = string.Empty,

            };


            var identityResult = await _userManager.CreateAsync(agentUser, request.Password);

            if (!identityResult.Succeeded)
            {
                throw new Exception($"Failed to create Agent.");
            }

            await _userManager.AddToRoleAsync(agentUser, "Agent");

            var agent = new Agent
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
            };

            if (request.Image != null && request.Image.Length > 0)
            {
                agent.ImageUrl = await _fileUploadService.UploadFileAsync(request.Image, "agents");
            }

            _unitOfWork.Repository<Agent>().Add(agent);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add agent");
            }

            return new AgentResponse
            {
                Id = agent.Id,
                Name = agent.Name,
                Email = agent.Email,
                Phone = agent.Phone,

            };

        }

        public async Task<IReadOnlyList<AgentResponse>> GetAllAgentsAsync(CancellationToken cancellationToken)
        {
            var agents = await _unitOfWork.Repository<Agent>()
                .GetAllAsync(cancellationToken);

            if (agents == null || !agents.Any())
                throw new Exception("No Agents Founded!");

            return agents.Select(agent => new AgentResponse
            {
                Id = agent.Id,
                Name = agent.Name,
                Email = agent.Email,
                Phone = agent.Phone,

            }).ToList().AsReadOnly();
        }
    }
}