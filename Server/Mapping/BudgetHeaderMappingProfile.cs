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

            CreateMap<BudgetAccount, BudgetAccountResponse>();


            CreateMap<BudgetTotalExpensesAndBudget, BudgetHeaderResponse>()
            .AfterMap((source, destination) =>
            {
                destination.RemainingBalance = destination.TotalBudget - destination.TotalExpenses;

                destination.ExpensePercent = Math.Round((destination.TotalExpenses / destination.TotalBudget) * 100, 2);
            });
        }
    }
}