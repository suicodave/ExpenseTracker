using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Expenses;

using Shared.Expenses;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/Expense")]
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
        public async Task<ActionResult> AddAccount(int expenseId, CreateExpenseAccountRequest request)
        {
            bool expenseExists = await _context.Expenses.AnyAsync(x => x.Id == expenseId);

            if (!expenseExists)
            {
                return NotFound();
            }

            ExpenseAccount account = _mapper.Map<ExpenseAccount>(request);

            account.ExpenseId = expenseId;

            _context.ExpenseAccounts.Add(account);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();
        }
    }
}