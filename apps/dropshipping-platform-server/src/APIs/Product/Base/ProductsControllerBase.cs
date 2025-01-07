using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProductsControllerBase : ControllerBase
{
    protected readonly IProductsService _service;

    public ProductsControllerBase(IProductsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateInput input)
    {
        var product = await _service.CreateProduct(input);

        return CreatedAtAction(nameof(Product), new { id = product.Id }, product);
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute()] ProductWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProduct(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Product>>> Products(
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        return Ok(await _service.Products(filter));
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProductsMeta(
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        return Ok(await _service.ProductsMeta(filter));
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Product>> Product([FromRoute()] ProductWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Product(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateProduct(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] ProductUpdateInput productUpdateDto
    )
    {
        try
        {
            await _service.UpdateProduct(uniqueId, productUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Orders records to Product
    /// </summary>
    [HttpPost("{Id}/orders")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.ConnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Orders records from Product
    /// </summary>
    [HttpDelete("{Id}/orders")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.DisconnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Orders records for Product
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<Order>>> FindOrders(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] OrderFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOrders(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Orders records for Product
    /// </summary>
    [HttpPatch("{Id}/orders")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.UpdateOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
