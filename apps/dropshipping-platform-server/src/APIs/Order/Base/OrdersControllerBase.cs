using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrdersControllerBase : ControllerBase
{
    protected readonly IOrdersService _service;

    public OrdersControllerBase(IOrdersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Order>> CreateOrder(OrderCreateInput input)
    {
        var order = await _service.CreateOrder(input);

        return CreatedAtAction(nameof(Order), new { id = order.Id }, order);
    }

    /// <summary>
    /// Delete one Order
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute()] OrderWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteOrder(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Order>>> Orders([FromQuery()] OrderFindManyArgs filter)
    {
        return Ok(await _service.Orders(filter));
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrdersMeta([FromQuery()] OrderFindManyArgs filter)
    {
        return Ok(await _service.OrdersMeta(filter));
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Order>> Order([FromRoute()] OrderWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Order(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateOrder(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] OrderUpdateInput orderUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrder(uniqueId, orderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple product records to Order
    /// </summary>
    [HttpPost("{Id}/product")]
    public async Task<ActionResult> ConnectProduct(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.ConnectProduct(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple product records from Order
    /// </summary>
    [HttpDelete("{Id}/product")]
    public async Task<ActionResult> DisconnectProduct(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromBody()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.DisconnectProduct(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple product records for Order
    /// </summary>
    [HttpGet("{Id}/product")]
    public async Task<ActionResult<List<Product>>> FindProduct(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindProduct(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple product records for Order
    /// </summary>
    [HttpPatch("{Id}/product")]
    public async Task<ActionResult> UpdateProduct(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromBody()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.UpdateProduct(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
