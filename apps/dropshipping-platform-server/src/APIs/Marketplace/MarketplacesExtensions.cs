using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.Infrastructure.Models;

namespace DropshippingPlatform.APIs.Extensions;

public static class MarketplacesExtensions
{
    public static Marketplace ToDto(this MarketplaceDbModel model)
    {
        return new Marketplace
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MarketplaceDbModel ToModel(
        this MarketplaceUpdateInput updateDto,
        MarketplaceWhereUniqueInput uniqueId
    )
    {
        var marketplace = new MarketplaceDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            marketplace.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            marketplace.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return marketplace;
    }
}
