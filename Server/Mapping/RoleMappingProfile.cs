using AutoMapper;

using Server.Users;

using Shared.Roles;

namespace Server.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponse>();
        }
    }
}