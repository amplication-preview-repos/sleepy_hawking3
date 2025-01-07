using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using DropshippingPlatform.APIs.Extensions;
using DropshippingPlatform.Infrastructure;
using DropshippingPlatform.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DropshippingPlatform.APIs;

public abstract class MarketplacesServiceBase : IMarketplacesService
{
    protected readonly DropshippingPlatformDbContext _context;

    public MarketplacesServiceBase(DropshippingPlatformDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Marketplace
    /// </summary>
    public async Task<Marketplace> CreateMarketplace(MarketplaceCreateInput createDto)
    {
        var marketplace = new MarketplaceDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            marketplace.Id = createDto.Id;
        }

        _context.Marketplaces.Add(marketplace);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MarketplaceDbModel>(marketplace.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Marketplace
    /// </summary>
    public async Task DeleteMarketplace(MarketplaceWhereUniqueInput uniqueId)
    {
        var marketplace = await _context.Marketplaces.FindAsync(uniqueId.Id);
        if (marketplace == null)
        {
            throw new NotFoundException();
        }

        _context.Marketplaces.Remove(marketplace);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Marketplaces
    /// </summary>
    public async Task<List<Marketplace>> Marketplaces(MarketplaceFindManyArgs findManyArgs)
    {
        var marketplaces = await _context
            .Marketplaces.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return marketplaces.ConvertAll(marketplace => marketplace.ToDto());
    }

    /// <summary>
    /// Meta data about Marketplace records
    /// </summary>
    public async Task<MetadataDto> MarketplacesMeta(MarketplaceFindManyArgs findManyArgs)
    {
        var count = await _context.Marketplaces.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Marketplace
    /// </summary>
    public async Task<Marketplace> Marketplace(MarketplaceWhereUniqueInput uniqueId)
    {
        var marketplaces = await this.Marketplaces(
            new MarketplaceFindManyArgs { Where = new MarketplaceWhereInput { Id = uniqueId.Id } }
        );
        var marketplace = marketplaces.FirstOrDefault();
        if (marketplace == null)
        {
            throw new NotFoundException();
        }

        return marketplace;
    }

    /// <summary>
    /// Update one Marketplace
    /// </summary>
    public async Task UpdateMarketplace(
        MarketplaceWhereUniqueInput uniqueId,
        MarketplaceUpdateInput updateDto
    )
    {
        var marketplace = updateDto.ToModel(uniqueId);

        _context.Entry(marketplace).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Marketplaces.Any(e => e.Id == marketplace.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
