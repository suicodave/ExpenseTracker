using AutoMapper;

using Server.Organizations;

using Shared.Organizations;

namespace Server.Mapping
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Organization, OrganizationResponse>();

            CreateMap<OrganizationRequest, Organization>();
        }
    }
}