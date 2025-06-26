using ProductApp.Domain.Common;

namespace ProductApp.Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}
