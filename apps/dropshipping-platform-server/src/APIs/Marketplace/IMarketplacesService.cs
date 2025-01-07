using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;

namespace DropshippingPlatform.APIs;

public interface IMarketplacesService
{
    /// <summary>
    /// Create one Marketplace
    /// </summary>
    public Task<Marketplace> CreateMarketplace(MarketplaceCreateInput marketplace);

    /// <summary>
    /// Delete one Marketplace
    /// </summary>
    public Task DeleteMarketplace(MarketplaceWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Marketplaces
    /// </summary>
    public Task<List<Marketplace>> Marketplaces(MarketplaceFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Marketplace records
    /// </summary>
    public Task<MetadataDto> MarketplacesMeta(MarketplaceFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Marketplace
    /// </summary>
    public Task<Marketplace> Marketplace(MarketplaceWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Marketplace
    /// </summary>
    public Task UpdateMarketplace(
        MarketplaceWhereUniqueInput uniqueId,
        MarketplaceUpdateInput updateDto
    );
}
