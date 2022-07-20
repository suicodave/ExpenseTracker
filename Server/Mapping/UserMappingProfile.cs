using AutoMapper;

using Server.Users;

using Shared.Member;
using Shared.Users;

namespace Server.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email)).IncludeAllDerived();

            CreateMap<CreateMemberRequest, User>();
        }
    }
}