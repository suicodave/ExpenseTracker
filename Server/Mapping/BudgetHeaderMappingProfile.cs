using AutoMapper;

using Server.Budgets;

using Shared.Budgets;

namespace Server.Mapping
{
    public class BudgetHeaderMappingProfile : Profile
    {
        public BudgetHeaderMappingProfile()
        {
            CreateMap<CreateBudgetHeaderRequest, BudgetHeader>();

            CreateMap<BudgetHeader, BudgetHeaderResponse>();

            CreateMap<BudgetAccountRequest, BudgetAccount>();
        }
    }
}