namespace DropshippingPlatform.APIs.Dtos;

public class ProductCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<Order>? Orders { get; set; }

    public DateTime UpdatedAt { get; set; }
}
