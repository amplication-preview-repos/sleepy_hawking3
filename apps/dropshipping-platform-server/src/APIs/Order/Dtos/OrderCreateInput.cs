namespace DropshippingPlatform.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<Product> Product { get; set; }

    public DateTime UpdatedAt { get; set; }
}
