using AutoMapper;

using Server.Budgets;

using Shared.BudgetTemplates;

namespace Server.Mapping
{
    public class BudgetTemplateMappingProfile : Profile
    {
        public BudgetTemplateMappingProfile()
        {
            CreateMap<CreateBudgetTemplateRequest, BudgetTemplate>();

            CreateMap<BudgetTemplate, BudgetTemplateResponse>();
        }
    }
}