using Server.Common.Entities;

namespace Server.Organizations
{
    public class Organization : AuditableEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}