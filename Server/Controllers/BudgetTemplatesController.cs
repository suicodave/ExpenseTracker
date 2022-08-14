using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Server.Budgets;
using Server.Data;
using Server.Organizations.Queries;
using Server.Users;

using Shared.BudgetTemplates;

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

        [HttpPost]
        [Authorize(Roles = "Owner")]
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
    }
}