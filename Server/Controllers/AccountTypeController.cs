using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.AccountTypes;
using Server.Data;

using Shared.AccountTypes;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountTypeController(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountTypes()
        {
            return Ok(await _context.AccountTypes
            .OrderByDescending(x => x.Id)
            .ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateAccountType(AccountTypeCreateRequest request)
        {
            AccountType accountType = new AccountType
            {
                Name = request.Name,
                Description = request.Description,
                OrganizationId = request.OrganizationId
            };

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