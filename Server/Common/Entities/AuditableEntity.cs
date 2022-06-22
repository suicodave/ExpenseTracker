using Server.Users;

namespace Server.Common.Entities
{
    public class AuditableEntity
    {
        public int UserId { get; set; }

        public User User { get; set; } = new();

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class AuditableEntity<TKey> : AuditableEntity
    where TKey : struct
    {
        public TKey Id { get; set; }
    }
}