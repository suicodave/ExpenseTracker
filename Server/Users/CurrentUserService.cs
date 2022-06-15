using System.Collections;
using System.Security.Claims;

namespace Server.Users
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        private ClaimsPrincipal? User => _httpContext?.HttpContext?.User;

        public string? Email => User?.Identity?.Name;

        public IEnumerable<Claim> Claims => User?.Claims.Select(x => new Claim(x.Type, x.Value)) ?? new List<Claim>();

        public int UserId => int.Parse(User?.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value ?? "0");

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
    }
}