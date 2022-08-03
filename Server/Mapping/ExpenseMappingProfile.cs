using AutoMapper;

using Server.Expenses;

using Shared.Expenses;

namespace Server.Mapping
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<CreateExpenseRequest, Expense>();

            CreateMap<Expense, ExpenseResponse>();

            CreateMap<CreateExpenseAccountRequest, ExpenseAccount>();

            CreateMap<ExpenseAccount, ExpenseAccountResponse>();
        }
    }
}