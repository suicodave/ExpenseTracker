using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Users;

using Shared.Roles;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRole request)
        {

            var result = await _roleManager.CreateAsync(new Role
            {
                Name = request.Name
            });

            if (result.Succeeded)
            {
                return Created("", null);
            }

            return BadRequest();

        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {

            var role = await _roleManager.FindByNameAsync(name);


            if (role is null)
            {
                return NotFound();
            }


            var result = await _roleManager.DeleteAsync(role);


            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();

        }


    }
}