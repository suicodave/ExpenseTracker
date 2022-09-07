
using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Budgets;
using Server.Data;
using Server.Organizations.Queries;
using Server.Users;

using Shared.BudgetTemplates;

using Server.Data.Extensions;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetTemplatesController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;

        private readonly ISender _mediator;

        public BudgetTemplatesController(
            IMapper mapper,
            ApplicationDbContext context,
            ISender mediator
        )
        {
            _mapper = mapper;
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetTemplateResponse>>> GetBudgetTemplates()
        {
            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (organization is null)
            {
                return Ok(Enumerable.Empty<BudgetTemplateResponse>());
            }

            return Ok(await _context.BudgetTemplates
            .FilterByOrganization(organization)
            .Include(x => x.AccountType)
            .OrderByDescending(x => x.IsActive)
            .ThenBy(x => x.AccountType.Name)
            .ProjectTo<BudgetTemplateResponse>(_mapper.ConfigurationProvider)
            .ToListAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Accountant")]
        public async Task<ActionResult> CreateBudgetTemplate(CreateBudgetTemplateRequest request)
        {
            BudgetTemplate budgetTemplate = _mapper.Map<BudgetTemplate>(request);

            UserOrganization? organization = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (organization is null)
            {
                return NotFound();
            }

            budgetTemplate.OrganizationId = organization.OrganizationId;

            _context.BudgetTemplates.Add(budgetTemplate);

            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 1)
            {
                return Created("", null);
            }

            return BadRequest();
        }

        [HttpPut("{budgetTemplateId}/Activate")]
        [Authorize(Roles = "Owner,Accountant")]
        public async Task<ActionResult> ActivateBudgetTemplate(int budgetTemplateId, ActivateBudgetRequest request, CancellationToken cancellationToken)
        {
            BudgetTemplate? budgetTemplate = await _context.BudgetTemplates.FindAsync(budgetTemplateId);

            UserOrganization? currentUserOrg = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (budgetTemplate is null || currentUserOrg is null)
            {
                return NotFound();
            }

            if (currentUserOrg.OrganizationId != budgetTemplate.OrganizationId)
            {
                return BadRequest();
            }

            budgetTemplate.IsActive = request.IsActive;

            int affectedRows = await _context.SaveChangesAsync(cancellationToken);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<BudgetTemplateResponse>>> GetActiveBudgetTemplates()
        {
            UserOrganization? currentUserOrg = await _mediator.Send(new GetCurrentOrganizationQuery());

            if (currentUserOrg is null)
            {
                return NotFound();
            }

            return Ok(await _context.BudgetTemplates
            .FilterByOrganization(currentUserOrg)
            .Include(x => x.AccountType)
            .Where(x => x.IsActive)
            .OrderBy(x => x.AccountType.Name)
            .ProjectTo<BudgetTemplateResponse>(_mapper.ConfigurationProvider)
            .ToListAsync());
        }
    }
}