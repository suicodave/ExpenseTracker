using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Organizations;
using Server.Users;

using Shared.Organizations;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _userService;
        private readonly ILogger<OrganizationsController> _logger;

        public OrganizationsController(ApplicationDbContext context, IMapper mapper, CurrentUserService userService, ILogger<OrganizationsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<int>> CreateOrganization([FromBody] OrganizationRequest request)
        {
            Organization organization = _mapper.Map<Organization>(request);

            bool hasDefaultOrganization = await _context.Organizations.AnyAsync(
                x => x.UserId == _userService.UserId && x.IsDefault
            );

            if (!hasDefaultOrganization)
            {
                organization.IsDefault = true;
            }

            _context.Organizations.Add(organization);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();
        }



        [HttpGet("Owned")]
        public async Task<ActionResult<IEnumerable<OrganizationResponse>>> GetUserOrganizations()
        {
            return Ok(
                await _context.Organizations.Where(x => x.UserId == _userService.UserId)
                .OrderByDescending(x => x.Id)
                .ProjectTo<OrganizationResponse>(_mapper.ConfigurationProvider)
                .ToListAsync()
            );
        }

        [HttpPut("{organizationId}/SetDefault")]
        public async Task<ActionResult<int>> SetDefaultOrganization(int organizationId)
        {
            Organization? selectedOrganization = await _context.Organizations.Where(x => x.UserId == _userService.UserId && x.Id == organizationId)
            .FirstOrDefaultAsync();

            Organization? currentOrganization = await _context.Organizations.Where(x => x.UserId == _userService.UserId && x.IsDefault)
            .FirstOrDefaultAsync();

            if (selectedOrganization is null)
            {
                return NotFound();
            }

            if (currentOrganization is not null)
            {
                currentOrganization.IsDefault = false;
            }

            selectedOrganization.IsDefault = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}