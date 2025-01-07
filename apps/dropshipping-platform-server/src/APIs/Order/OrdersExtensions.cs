using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.Infrastructure.Models;

namespace DropshippingPlatform.APIs.Extensions;

public static class OrdersExtensions
{
    public static Order ToDto(this OrderDbModel model)
    {
        return new Order
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Product = model.Product?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OrderDbModel ToModel(
        this OrderUpdateInput updateDto,
        OrderWhereUniqueInput uniqueId
    )
    {
        var order = new OrderDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            order.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            order.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return order;
    }
}
