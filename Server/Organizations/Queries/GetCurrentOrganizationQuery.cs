using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Users;

namespace Server.Organizations.Queries
{
    public class GetCurrentOrganizationQuery : IRequest<UserOrganization?>
    {
        sealed class GetCurrentOrganizationQueryHandler : IRequestHandler<GetCurrentOrganizationQuery, UserOrganization?>
        {
            private readonly ApplicationDbContext _context;
            private readonly CurrentUserService _currentUser;

            public GetCurrentOrganizationQueryHandler(
                ApplicationDbContext context,
                CurrentUserService currentUser
            )
            {
                _context = context;
                _currentUser = currentUser;
            }

            public Task<UserOrganization?> Handle(GetCurrentOrganizationQuery request, CancellationToken cancellationToken)
            {
                return _context.UserOrganizations.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId && x.IsDefault);
            }
        }
    }


}