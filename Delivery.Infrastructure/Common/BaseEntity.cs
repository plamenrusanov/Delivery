namespace Delivery.Infrastructure.Common
{
    public abstract class BaseEntity<TKey> : IAuditInfo
    {
        public abstract TKey Id { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? ModifiedOn { get; set; }
    }
}
