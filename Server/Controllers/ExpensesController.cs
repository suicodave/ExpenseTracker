using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.Data.Extensions;
using Server.Expenses;
using Server.Organizations.Queries;
using Server.Users;

using Shared.Common.Expenses;
using Shared.Expenses;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public ExpensesController(ApplicationDbContext context, IMapper mapper, ISender mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("Pending")]
        public async Task<ActionResult<IEnumerable<ExpenseResponse>>> GetPendingExpenses()
        {
            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (organization is null)
            {
                return Ok(Enumerable.Empty<ExpenseResponse>());
            }

            return Ok(await _context.Expenses
            .FilterByOrganization(organization)
            .Where(x => x.Status == ExpenseStatus.Pending)
            .Include(x => x.Accounts)
            .ThenInclude(x => x.AccountType)
            .ProjectTo<ExpenseResponse>(_mapper.ConfigurationProvider).ToListAsync());

        }

        [HttpGet("Completed")]
        public async Task<ActionResult<IEnumerable<ExpenseResponse>>> GetCompletedExpenses()
        {
            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (organization is null)
            {
                return Ok(Enumerable.Empty<ExpenseResponse>());
            }

            return Ok(await _context.Expenses
            .FilterByOrganization(organization)
            .Where(x => x.Status == ExpenseStatus.Completed)
            .Include(x => x.Accounts)
            .ThenInclude(x => x.AccountType)
            .ProjectTo<ExpenseResponse>(_mapper.ConfigurationProvider).ToListAsync());

        }

        [HttpPost]
        [Authorize(Roles = "Owner,Clerk")]
        public async Task<ActionResult> CreateExpense(CreateExpenseRequest request)
        {
            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (organization is null)
            {
                return BadRequest();
            }

            Expense expense = _mapper.Map<Expense>(request);

            expense.Status = ExpenseStatus.Pending;

            expense.OrganizationId = organization.OrganizationId;

            _context.Expenses.Add(expense);

            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Created("", null);
            }

            return BadRequest();
        }

        [HttpPut("{expenseId}/Complete")]
        [Authorize(Roles = "Owner,Accountant")]
        public async Task<ActionResult> CompleteExpense(int expenseId, CancellationToken cancellationToken)
        {
            Expense? expense = await _context.Expenses
            .Include(x => x.Accounts)
            .FirstOrDefaultAsync(x => x.Id == expenseId);

            if (expense is null)
            {
                return NotFound();
            }

            if (expense.Amount != expense.Accounts.Sum(x => x.Amount) && expense.Status != ExpenseStatus.Pending)
            {
                return BadRequest();
            }

            expense.Status = ExpenseStatus.Completed;

            int affectedRows = await _context.SaveChangesAsync(cancellationToken);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}