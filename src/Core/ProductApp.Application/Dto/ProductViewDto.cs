namespace ProductApp.Application.Dto;

public class ProductViewDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }
}
