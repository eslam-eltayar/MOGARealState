using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOGARealState.Core.DTOs.Requests;
using MOGARealState.Core.DTOs.Responses;
using MOGARealState.Core.Entities;
using MOGARealState.Core.Services;

namespace MOGARealState.APIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAgentService _agentService;

        public AuthController(
            UserManager<AppUser> userManager, 
            ITokenService tokenService, 
            SignInManager<AppUser> signInManager,
            IAgentService agentService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _agentService = agentService;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto model)
        {

            var existingEmail = await _userManager.Users.AnyAsync(u => u.Email == model.Email);
            var existingUsername = await _userManager.Users.AnyAsync(u => u.UserName == model.UserName);

            if (existingEmail)
                return BadRequest(new { Message = "Email is already registered." });

            if (existingUsername)
                return BadRequest(new { Message = "Username is already taken." });


            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName,
                City = model.City,
                PhoneNumber = model.Phone,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "User registration failed." });
            }

            await _userManager.AddToRoleAsync(user, "User");

            var returnedUser = new UserDto()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(returnedUser);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return Unauthorized(new { Message = "Invalid Login" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new { Message = "Invalid Login" });

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault();

            int? agentId = null;

            if (role == "Agent")
            {
                agentId = await _agentService.GetAgentIdByEmailAsync(user.Email!);
            }

            return Ok(new UserDto()
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                Role = role ?? "",
                AgentId = agentId
            });
        }
    }
}
