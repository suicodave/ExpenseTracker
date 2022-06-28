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
    [Authorize(Roles = "Owner")]
    public class OrganizationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _userService;

        public OrganizationsController(ApplicationDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
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


        [HttpGet("Owned")]
        public async Task<ActionResult<IEnumerable<OrganizationResponse>>> GetUserOrganizations()
        {
            return Ok(
                await _context.Organizations.Where(x => x.UserId == _userService.UserId)
                .ProjectTo<OrganizationResponse>(_mapper.ConfigurationProvider)
                .ToListAsync()
            );
        }
    }
}