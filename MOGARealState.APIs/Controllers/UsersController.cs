using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGARealState.Core.DTOs.Requests;
using MOGARealState.Core.DTOs.Responses;
using MOGARealState.Core.Services;

namespace MOGARealState.APIs.Controllers
{
    public class UsersController(IUserService userService) : ApiBaseController
    {
        private readonly IUserService _userService = userService;

        [HttpPost("FavoriteProperty/{userId}/{propertyId}")]
        public async Task<IActionResult> FavoriteProperty(string userId, int propertyId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.FavoritePropertyAsync(userId, propertyId, cancellationToken);

                return Ok(new { Message = "Property Added To Favorites Successfully." });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("IsFavorite/{userId}/{propertyId}")]
        public async Task<IActionResult> IsFavoriteProperty(string userId, int propertyId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.IsFavoritePropertyAsync(userId, propertyId, cancellationToken);

                return Ok(new { isFavorite = result });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("Favorite/{userId}/{propertyId}")]
        public async Task<IActionResult> DeleteFavoriteProperty(string userId, int propertyId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.DeleteFavoritePropertyAsync(userId, propertyId, cancellationToken);

                return Ok(new { Message = "Property Deleted From Favorites Successfully" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("FavoriteProperties/{userId}")]
        public async Task<ActionResult<IReadOnlyList<AllPropertiesResponse>>> FavoriteProperties(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var props = await _userService.GetFavoritePropertiesAsync(userId, cancellationToken);

                return Ok(props);
            }
            catch (Exception ex)
            {

                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("OrderProperty/{userId}/{propertyId}")]
        public async Task<IActionResult> OrderProperty(string userId, int propertyId, CancellationToken cancellationToken)
        {
            try
            {
                var props = await _userService.OrderPropertyAsync(userId, propertyId, cancellationToken);

                return Ok(new { Message = "Property Ordered Successfully" });
            }
            catch (Exception ex)
            {

                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("UserData/{userId}")]
        public async Task<ActionResult<UserDataResponse>> GetUserData(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _userService.GetUserDataAsync(userId, cancellationToken);

                return Ok(data);
            }
            catch (Exception ex)
            {

                return NotFound(new { Message = ex.Message });
            }
        }

    }
}
