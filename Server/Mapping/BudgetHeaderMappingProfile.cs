using AutoMapper;

using Server.Budgets;

using Shared.BudgetHeaders;

namespace Server.Mapping
{
    public class BudgetHeaderMappingProfile : Profile
    {
        public BudgetHeaderMappingProfile()
        {
            CreateMap<BudgetHeaderRequest, BudgetHeader>();

            CreateMap<BudgetHeader, BudgetHeaderResponse>();
        }
    }
}