using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SuppliersControllerBase : ControllerBase
{
    protected readonly ISuppliersService _service;

    public SuppliersControllerBase(ISuppliersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Supplier
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Supplier>> CreateSupplier(SupplierCreateInput input)
    {
        var supplier = await _service.CreateSupplier(input);

        return CreatedAtAction(nameof(Supplier), new { id = supplier.Id }, supplier);
    }

    /// <summary>
    /// Delete one Supplier
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSupplier([FromRoute()] SupplierWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSupplier(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Suppliers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Supplier>>> Suppliers(
        [FromQuery()] SupplierFindManyArgs filter
    )
    {
        return Ok(await _service.Suppliers(filter));
    }

    /// <summary>
    /// Meta data about Supplier records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SuppliersMeta(
        [FromQuery()] SupplierFindManyArgs filter
    )
    {
        return Ok(await _service.SuppliersMeta(filter));
    }

    /// <summary>
    /// Get one Supplier
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Supplier>> Supplier(
        [FromRoute()] SupplierWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Supplier(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Supplier
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSupplier(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromQuery()] SupplierUpdateInput supplierUpdateDto
    )
    {
        try
        {
            await _service.UpdateSupplier(uniqueId, supplierUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
