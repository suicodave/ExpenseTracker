using AutoMapper;

using Server.Users;

using Shared.Users;

namespace Server.Mapping
{
    public class UserOrganizationMappingProfile : Profile
    {
        public UserOrganizationMappingProfile()
        {
            CreateMap<UserOrganization,UserOrganizationResponse>();
        }
    }
}