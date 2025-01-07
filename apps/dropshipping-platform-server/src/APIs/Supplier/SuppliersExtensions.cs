using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.Infrastructure.Models;

namespace DropshippingPlatform.APIs.Extensions;

public static class SuppliersExtensions
{
    public static Supplier ToDto(this SupplierDbModel model)
    {
        return new Supplier
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SupplierDbModel ToModel(
        this SupplierUpdateInput updateDto,
        SupplierWhereUniqueInput uniqueId
    )
    {
        var supplier = new SupplierDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            supplier.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            supplier.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return supplier;
    }
}
