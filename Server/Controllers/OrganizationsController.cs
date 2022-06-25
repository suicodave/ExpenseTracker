using AutoMapper;
using AutoMapper.QueryableExtensions;

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
        private readonly IMapper _mapper;

        public OrganizationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrganization([FromBody] OrganizationRequest request)
        {

            Organization organization = _mapper.Map<Organization>(request);

            _context.Organizations.Add(organization);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationResponse>>> GetAll()
        {
            return Ok(await _context.Organizations.OrderByDescending(x => x.Id)
            .ProjectTo<OrganizationResponse>(_mapper.ConfigurationProvider)
            .ToListAsync());
        }
    }
}