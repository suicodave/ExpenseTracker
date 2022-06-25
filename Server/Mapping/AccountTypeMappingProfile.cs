using AutoMapper;

using Server.AccountTypes;

using Shared.AccountTypes;

namespace Server.Mapping
{
    public class AccountTypeMappingProfile : Profile
    {
        public AccountTypeMappingProfile()
        {
            CreateMap<AccountType, AccountTypeResponse>();

            CreateMap<AccountTypeRequest, AccountType>();
        }
    }
}