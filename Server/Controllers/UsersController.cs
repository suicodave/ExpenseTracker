using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Server.Users;

using Shared.Users;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly CurrentUserService _currentUserService;

        public UsersController(
            UserManager<User> userManager,
            CurrentUserService currentUserService
        )
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<ActionResult> Register(RegisterUserRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                DisplayName = request.DisplayName
            };

            var createdUser = await _userManager.CreateAsync(user, request.Password);

            await _userManager.AddToRoleAsync(user, Role.OWNER);

            if (!createdUser.Succeeded)
            {
                var errors = createdUser.Errors.Select(x => x.Description);

                return BadRequest(new CreateUserResponse
                {
                    IsSuccessful = createdUser.Succeeded,
                    Errors = errors
                });
            }

            return Created("", new CreateUserResponse { IsSuccessful = createdUser.Succeeded });
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public ActionResult CurrentUser()
        {
            return Ok(new
            {
                Name = _currentUserService.Email,
                Id = _currentUserService.UserId,
                Claims = _currentUserService.Claims
            });
        }
    }
}