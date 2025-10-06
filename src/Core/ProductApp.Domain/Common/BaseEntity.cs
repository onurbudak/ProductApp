namespace ProductApp.Domain.Common;

public class BaseEntity<TId> : IEntity where TId : notnull
{
    public TId Id { get; set; }

    public DateTime CreateDate { get; set; }

    public long CreateUserId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? UpdateUserId { get; set; }

    public DateTime? DeleteDate { get; set; }

    public long? DeleteUserId { get; set; }

    public bool IsDeleted { get; set; }
}
