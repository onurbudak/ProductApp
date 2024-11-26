namespace ProductApp.Domain.Common;

public class BaseEntity
{
    public long Id { get; set; }

    public DateTime CreateDate { get; set; }

    public long CreateUserId { get; set; } 

    public DateTime? UpdateDate { get; set; }

    public long? UpdateUserId { get; set; }

    public DateTime? DeleteDate { get; set; }

    public long? DeleteUserId { get; set; }

    public bool IsDeleted { get; set; }
}
