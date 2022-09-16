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
            .ForMember(dest => dest.Budget, options => options.MapFrom(source => source.TotalBudget))
            .ForMember(dest => dest.Expenses, options => options.MapFrom(source => source.TotalExpenses))
            .AfterMap((source, destination) =>
            {
                destination.Balance = destination.Budget - destination.Expenses;

                destination.Percent = Math.Round((destination.Expenses / destination.Budget) * 100, 2);
            });

            CreateMap<BudgetAccountWithExpense, BudgetAccountWithExpenseResponse>()
            .ForMember(dest => dest.Expenses, options => options.MapFrom(source => source.TotalExpenses))
            .AfterMap((source, destination) =>
            {
                destination.Percent = Math.Round((destination.Expenses / destination.Budget) * 100, 2);

                destination.Balance = destination.Budget - destination.Expenses;
            });

            CreateMap<BudgetUntrackedAccount, BudgetUntrackedAccountResponse>();
        }
    }
}