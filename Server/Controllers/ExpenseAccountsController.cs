using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Expenses;

using Shared.Common.Expenses;
using Shared.Expenses;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/Expenses")]
    [Authorize]
    public class ExpenseAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseAccountsController(
            ApplicationDbContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpPost("{expenseId}/Accounts")]
        [Authorize(Roles = "Accountant,Owner")]
        public async Task<ActionResult> AddAccount(int expenseId, CreateExpenseAccountRequest request, CancellationToken cancellationToken)
        {
            bool expenseExists = await _context.Expenses.AnyAsync(x => x.Id == expenseId);

            if (!expenseExists)
            {
                return NotFound();
            }

            ExpenseAccount account = _mapper.Map<ExpenseAccount>(request);

            account.ExpenseId = expenseId;

            _context.ExpenseAccounts.Add(account);

            int result = await _context.SaveChangesAsync(cancellationToken: cancellationToken);

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();
        }

        [HttpDelete("Accounts/{accountId}")]
        [Authorize(Roles = "Accountant,Owner")]
        public async Task<ActionResult> DeleteAccount(int accountId)
        {

            var account = await _context.ExpenseAccounts
            .Include(x => x.Expense)
            .FirstOrDefaultAsync(x => x.Id == accountId);

            if (account is null)
            {
                return NotFound();
            }

            if (account.Expense.Status != ExpenseStatus.Pending)
            {
                return BadRequest();
            }

            _context.ExpenseAccounts.Remove(account);

            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 1)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}