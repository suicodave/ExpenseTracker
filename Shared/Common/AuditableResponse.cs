namespace Shared.Common
{
    public class AuditableEntityResponse
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CreatedBy { get; set; }
    }

    public class AuditableEntityResponse<TKey> : AuditableEntityResponse
    where TKey : struct
    {
        public TKey Id { get; set; }
    }
}