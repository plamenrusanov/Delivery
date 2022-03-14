namespace Delivery.Infrastructure.Common
{
    public abstract class BaseDeletableEntity<TKey> : BaseEntity<TKey>, IDeletableEntity
    {
        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? DeletedOn { get; set; }
    }
}
