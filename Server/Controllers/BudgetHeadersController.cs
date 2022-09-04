using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Budgets;
using Server.Data;
using Server.Data.Extensions;
using Server.Organizations.Queries;
using Server.Users;

using Shared.Budgets;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetHeadersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly ApplicationDbContext _context;

        public BudgetHeadersController(
            IMapper mapper,
            ISender mediator,
            ApplicationDbContext context
        )
        {
            _mapper = mapper;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetHeaderResponse>>> GetBudgetHeaders()
        {
            UserOrganization? userOrganization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (userOrganization is null)
            {
                return NotFound();
            }

            IEnumerable<BudgetTotalExpensesAndBudget> budgetHeadersExpensesAndBudget = await _context.BudgetTotalExpensesAndBudget
            .FromSqlRaw(@"select 
            a.Id,
            a.CoveredFrom,
            a.CoveredTo,
            a.OrganizationId,

            (select isnull(sum(b.amount),0) from Expenses b where b.EffectiveDate between a.CoveredFrom and a.CoveredTo and b.OrganizationId = a.OrganizationId and Status='Completed' ) TotalExpenses,
            (select sum(b.amount) from BudgetAccounts b where b.BudgetHeaderId = a.Id ) TotalBudget

            from BudgetHeaders a")
            .FilterByOrganization(userOrganization)
            .OrderByDescending(x => x.CoveredFrom)
            .ThenByDescending(x => x.CoveredTo)
            .ToListAsync();

            var budgetHeaders = _mapper.Map<IEnumerable<BudgetHeaderResponse>>(budgetHeadersExpensesAndBudget);

            return Ok(budgetHeaders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBudgetHeader(CreateBudgetHeaderRequest request)
        {
            UserOrganization? userOrganization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (userOrganization is null)
            {
                return BadRequest();
            }

            BudgetHeader budgetHeader = _mapper.Map<BudgetHeader>(request);

            budgetHeader.OrganizationId = userOrganization.OrganizationId;

            _context.BudgetHeaders.Add(budgetHeader);

            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 1)
            {
                return Created("", null);
            }

            return BadRequest();
        }
    }
}