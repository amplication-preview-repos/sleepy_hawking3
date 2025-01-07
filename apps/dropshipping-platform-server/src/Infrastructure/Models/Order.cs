using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropshippingPlatform.Infrastructure.Models;

[Table("Orders")]
public class OrderDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<ProductDbModel> Product { get; set; } = new List<ProductDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
