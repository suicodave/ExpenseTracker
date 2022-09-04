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

            CreateMap<BudgetHeader, BudgetHeaderResponse>()
            .ForMember(dest => dest.TotalBudget, options => options.MapFrom(x => x.BudgetAccounts.Sum(x => x.Amount)));

            CreateMap<BudgetAccountRequest, BudgetAccount>();

            CreateMap<BudgetAccount, BudgetAccountResponse>();
        }
    }
}