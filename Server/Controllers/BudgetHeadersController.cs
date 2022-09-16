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

        [HttpGet("{budgetHeaderId}")]
        public async Task<ActionResult<IEnumerable<BudgetHeaderResponse>>> GetBudgetHeader(int budgetHeaderId)
        {
            var budgetHeader = await _context.BudgetTotalExpensesAndBudget
            .FromSqlRaw(@"
            select 
            a.Id,
            a.CoveredFrom,
            a.CoveredTo,
            a.OrganizationId,

            (select isnull(sum(b.amount),0) from Expenses b where b.EffectiveDate between a.CoveredFrom and a.CoveredTo and b.OrganizationId = a.OrganizationId and Status='Completed' ) TotalExpenses,
            (select sum(b.amount) from BudgetAccounts b where b.BudgetHeaderId = a.Id ) TotalBudget

            from BudgetHeaders a")
            .FirstOrDefaultAsync(x => x.Id == budgetHeaderId);

            if (budgetHeader is null)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<BudgetHeaderResponse>(budgetHeader);

            return Ok(mappedResponse);
        }


        [HttpGet("{budgetHeaderId}/Accounts")]
        public async Task<ActionResult<IEnumerable<BudgetAccountWithExpenseResponse>>> GetBudgetAccountsWithExpense(int budgetHeaderId)
        {
            var budgetAccountsWithExpense = await _context.BudgetAccountsWithExpense.FromSqlRaw(@"
            with CompletedExpenseAccounts as (
            select 

            ea.AccountTypeId, 
            ea.Amount ExpenseAmount,
            e.OrganizationId,
            e.Status,
            e.Id ExpenseId,
            e.EffectiveDate


            from ExpenseAccounts ea

            inner join expenses e on ea.ExpenseId = e.Id

            where e.Status = 'Completed'
            ),

            ScopedBudgetAccounts as 
            (
                select 

                ba.AccountTypeId,
                ba.Amount as BudgetAmount,
                ba.BudgetHeaderId,
                bh.OrganizationId,
                bh.CoveredFrom,
                bh.CoveredTo
                
                from BudgetAccounts ba

                inner join BudgetHeaders bh on ba.BudgetHeaderId = bh.Id
                
                
            ),

            AccountGroupedExpenses as (

            select  
            cea.AccountTypeId,
            sum(cea.ExpenseAmount) ExpenseAmount,
            cea.OrganizationId,
            cea.EffectiveDate

            from CompletedExpenseAccounts cea
            group by cea.AccountTypeId,OrganizationId,EffectiveDate

            )

			select 
			
			(select at.Name from AccountTypes at where sba.AccountTypeId = at.Id) AccountTypeName,
			sba.BudgetAmount Budget,
			(select ISNULL(sum(age.ExpenseAmount),0) from AccountGroupedExpenses age where sba.AccountTypeId = age.AccountTypeId
			and age.EffectiveDate between sba.CoveredFrom and sba.CoveredTo
			and sba.OrganizationId = age.OrganizationId
			) TotalExpenses
			
			
			from ScopedBudgetAccounts sba

			where sba.BudgetHeaderId = {0}

			order by AccountTypeName", budgetHeaderId)
            .ToListAsync();

            var response = _mapper.Map<IEnumerable<BudgetAccountWithExpenseResponse>>(budgetAccountsWithExpense);

            return Ok(response);
        }

        [HttpGet("{budgetHeaderId}/Accounts/Untracked")]
        public async Task<ActionResult<IEnumerable<BudgetUntrackedAccountResponse>>> GetUntrackedAccounts(int budgetHeaderId)
        {

            var budgetUntrackedAccounts = await _context.BudgetUntrackedAccounts.FromSqlRaw(@"
            with CompletedExpenseAccounts as (
            select 

            ea.AccountTypeId, 
            ea.Amount ExpenseAmount,
            e.OrganizationId,
            e.Status,
            e.Id ExpenseId,
            e.EffectiveDate


            from ExpenseAccounts ea

            inner join expenses e on ea.ExpenseId = e.Id

            where e.Status = 'Completed'
            ),

            ScopedBudgetAccounts as 
            (
                select 

                ba.AccountTypeId,
                ba.Amount as BudgetAmount,
                ba.BudgetHeaderId,
                bh.OrganizationId,
                bh.CoveredFrom,
                bh.CoveredTo
                
                from BudgetAccounts ba

                inner join BudgetHeaders bh on ba.BudgetHeaderId = bh.Id
            ),

            AccountGroupedExpenses as (

            select  
            cea.AccountTypeId,
            sum(cea.ExpenseAmount) ExpenseAmount,
            cea.OrganizationId,
            cea.EffectiveDate

            from CompletedExpenseAccounts cea
            group by cea.AccountTypeId,OrganizationId,EffectiveDate

            )

			select  
			
			(select at.Name from AccountTypes at where age.AccountTypeId = at.Id) AccountTypeName,
			sum(age.ExpenseAmount) Expense
			
			from AccountGroupedExpenses age where exists(
			
			select 1 from ScopedBudgetAccounts sba where age.EffectiveDate between sba.CoveredFrom and sba.CoveredTo

			and sba.BudgetHeaderId = {0}

			and age.OrganizationId = sba.OrganizationId
			)

			and not exists(
			
			select 1 from ScopedBudgetAccounts sba where sba.BudgetHeaderId = {0}
			
			and age.AccountTypeId = sba.AccountTypeId
			
			)

			group by age.AccountTypeId", budgetHeaderId)
            .ToListAsync();


            var response = _mapper.Map<IEnumerable<BudgetUntrackedAccountResponse>>(budgetUntrackedAccounts);


            return Ok(response);
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