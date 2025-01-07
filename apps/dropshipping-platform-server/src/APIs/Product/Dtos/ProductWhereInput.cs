namespace DropshippingPlatform.APIs.Dtos;

public class ProductWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<string>? Orders { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
