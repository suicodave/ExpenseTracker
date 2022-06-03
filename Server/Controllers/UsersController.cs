using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Shared.Users;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(
            UserManager<IdentityUser> userManager
        )
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> Create(CreateUserRequest request)
        {
            var user = new IdentityUser
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
    }
}