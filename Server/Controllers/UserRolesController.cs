using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Users;

using Shared.Users;

namespace Server.Controllers
{
    [ApiController]
    [Route("Users/Roles")]
    [Authorize]
    public class UserRolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserRolesController(
            RoleManager<Role> roleManager,
            UserManager<User> userManager
        )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SyncUserRole(SyncUserRoleRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            var roleNames = await _roleManager.Roles.Select(x => x.Name).ToListAsync();

            var validRoleNames = request.RoleNames.Intersect(roleNames, StringComparer.OrdinalIgnoreCase);

            if (user is null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            var result = await _userManager.AddToRolesAsync(user, validRoleNames);

            if (result.Succeeded)
            {
                return Created("", null);
            }

            return BadRequest(result.Errors);
        }
    }
}