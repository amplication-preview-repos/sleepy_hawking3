namespace DropshippingPlatform.APIs.Dtos;

public class OrderWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<string>? Product { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
