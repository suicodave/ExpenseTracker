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

        public UsersController(
            UserManager<User> userManager
        )
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> Create(CreateUserRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email
            };

            var createdUser = await _userManager.CreateAsync(user, request.Password);

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
                Name = HttpContext.User.Identity?.Name,
                Claims = HttpContext.User.Claims.Select(x => new Claim(x.Type, x.Value))
            });
        }
    }
}