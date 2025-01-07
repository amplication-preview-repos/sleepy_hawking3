using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MarketplacesControllerBase : ControllerBase
{
    protected readonly IMarketplacesService _service;

    public MarketplacesControllerBase(IMarketplacesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Marketplace
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Marketplace>> CreateMarketplace(MarketplaceCreateInput input)
    {
        var marketplace = await _service.CreateMarketplace(input);

        return CreatedAtAction(nameof(Marketplace), new { id = marketplace.Id }, marketplace);
    }

    /// <summary>
    /// Delete one Marketplace
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteMarketplace(
        [FromRoute()] MarketplaceWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteMarketplace(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Marketplaces
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Marketplace>>> Marketplaces(
        [FromQuery()] MarketplaceFindManyArgs filter
    )
    {
        return Ok(await _service.Marketplaces(filter));
    }

    /// <summary>
    /// Meta data about Marketplace records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MarketplacesMeta(
        [FromQuery()] MarketplaceFindManyArgs filter
    )
    {
        return Ok(await _service.MarketplacesMeta(filter));
    }

    /// <summary>
    /// Get one Marketplace
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Marketplace>> Marketplace(
        [FromRoute()] MarketplaceWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Marketplace(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Marketplace
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateMarketplace(
        [FromRoute()] MarketplaceWhereUniqueInput uniqueId,
        [FromQuery()] MarketplaceUpdateInput marketplaceUpdateDto
    )
    {
        try
        {
            await _service.UpdateMarketplace(uniqueId, marketplaceUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
