using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Organizations.Queries;
using Server.Users;

using Shared.Member;
using Shared.Users;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISender _mediator;

        public MembersController(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, ISender mediator)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetMembers()
        {
            UserOrganization? currentOrganization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (currentOrganization is null)
            {
                return Ok(Enumerable.Empty<UserResponse>());
            }

            var members = await _context.Users.Where(x => x.UserOrganizations.Any(xx => xx.OrganizationId == currentOrganization.OrganizationId))
            .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult> AddMember(CreateMemberRequest request)
        {
            User user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                UserOrganization? currentOrganization = await _mediator.Send(new GetCurrentOrganizationQuery());

                await _userManager.AddToRoleAsync(user, request.RoleName);

                UserOrganization userOrganization = new UserOrganization
                {
                    UserId = user.Id,
                    OrganizationId = currentOrganization!.Id,
                    IsDefault = true
                };

                _context.UserOrganizations.Add(userOrganization);

                await _context.SaveChangesAsync();

                return Created("", null);
            }

            return BadRequest();
        }
    }
}