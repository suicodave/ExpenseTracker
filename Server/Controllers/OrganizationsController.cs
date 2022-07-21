using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Organizations;
using Server.Organizations.Queries;
using Server.Users;

using Shared.Organizations;
using Shared.Users;

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
        private readonly ISender _mediator;

        public OrganizationsController(ApplicationDbContext context, IMapper mapper, CurrentUserService userService, ILogger<OrganizationsController> logger, ISender mediator)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<int>> CreateOrganization([FromBody] OrganizationRequest request)
        {
            Organization organization = _mapper.Map<Organization>(request);

            bool hasDefaultOrganization = await _context.Organizations.AnyAsync(
                x => x.UserOrganizations.Any(xx => xx.UserId == _userService.UserId && xx.IsDefault)
            );

            var userOrganization = new UserOrganization
            {
                UserId = (int)_userService.UserId!,
            };

            if (!hasDefaultOrganization)
            {
                userOrganization.IsDefault = true;
            }

            organization.UserOrganizations.Add(userOrganization);

            _context.Organizations.Add(organization);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();
        }



        [HttpGet("Owned")]
        public async Task<ActionResult<IEnumerable<UserOrganizationResponse>>> GetUserOrganizations()
        {
            return Ok(
                await _context.UserOrganizations
                .Include(x => x.Organization)
                .Where(y => y.UserId == _userService.UserId)
                .OrderByDescending(x => x.Id)
                .ProjectTo<UserOrganizationResponse>(_mapper.ConfigurationProvider)
                .ToListAsync()
            );
        }

        [HttpPut("{userOrganizationId}/SetDefault")]
        public async Task<ActionResult<int>> SetDefaultOrganization(int userOrganizationId)
        {
            UserOrganization? selectedOrganization = await _context.UserOrganizations.FirstOrDefaultAsync(x => x.Id == userOrganizationId);

            UserOrganization? currentOrganization = await _mediator.Send(new GetCurrentOrganizationQuery());

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