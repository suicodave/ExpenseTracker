using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Organizations;

using Shared.Organizations;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner")]
    public class OrganizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrganizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrganization([FromBody] CreateOrganizationRequest request)
        {

            var organization = new Organization
            {
                Name = request.Name
            };

            _context.Organizations.Add(organization);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetAll()
        {
            return Ok(await _context.Organizations.OrderByDescending(x => x.Id).ToListAsync());
        }
    }
}