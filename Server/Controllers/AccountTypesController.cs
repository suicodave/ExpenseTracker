using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.AccountTypes;
using Server.Data;
using Server.Organizations.Queries;
using Server.Users;

using Shared.AccountTypes;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public AccountTypesController(
            ApplicationDbContext context,
            IMapper mapper,
            ISender mediator
        )
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountTypeResponse>>> GetAccountTypes()
        {
            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (organization is null)
            {
                return Ok(Enumerable.Empty<AccountTypeResponse>());
            }

            return Ok(await _context.AccountTypes
            .Where(x => x.OrganizationId == organization.OrganizationId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<AccountTypeResponse>(_mapper.ConfigurationProvider)
            .ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateAccountType(AccountTypeRequest request)
        {
            AccountType accountType = _mapper.Map<AccountType>(request);

            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            accountType.OrganizationId = organization?.Id ?? 0;

            _context.AccountTypes.Add(accountType);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", result);
            }

            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAccountType(int id)
        {
            var accountType = await _context.AccountTypes.FindAsync(id);

            if (accountType is null)
            {
                return NotFound();
            }

            _context.Remove<AccountType>(accountType);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}