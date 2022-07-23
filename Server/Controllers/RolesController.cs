using AutoMapper;
using AutoMapper.QueryableExtensions;

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
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetRoles()
        {
            return Ok(await _roleManager.Roles
            .ProjectTo<RoleResponse>(_mapper.ConfigurationProvider)
            .ToListAsync());
        }

        [HttpGet("Members")]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAvailableMemberRoles()
        {
            var excludedRoles = new string[] { Role.ADMINISTRATOR, Role.OWNER };

            return Ok(await _roleManager.Roles
            .Where(x=> !excludedRoles.Any(xx=>xx == x.Name))
            .ProjectTo<RoleResponse>(_mapper.ConfigurationProvider)
            .ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleRequest request)
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