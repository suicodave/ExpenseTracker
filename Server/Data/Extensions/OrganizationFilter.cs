using Server.Common.Entities;
using Server.Users;

namespace Server.Data.Extensions
{
    public static class OrganizationFilter
    {
        public static IQueryable<TEntity> FilterByOrganization<TEntity>(this IQueryable<TEntity> queryable, UserOrganization organization)
        where TEntity : IOrganizationDependentEntity

        {
            return queryable.Where(x => x.OrganizationId == organization.OrganizationId);
        }


    }
}