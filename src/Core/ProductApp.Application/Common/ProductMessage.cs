namespace ProductApp.Application.Common;

public class ProductMessage
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }
}