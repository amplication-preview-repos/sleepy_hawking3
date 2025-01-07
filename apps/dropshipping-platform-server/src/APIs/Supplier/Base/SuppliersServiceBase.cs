using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using DropshippingPlatform.APIs.Extensions;
using DropshippingPlatform.Infrastructure;
using DropshippingPlatform.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DropshippingPlatform.APIs;

public abstract class SuppliersServiceBase : ISuppliersService
{
    protected readonly DropshippingPlatformDbContext _context;

    public SuppliersServiceBase(DropshippingPlatformDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Supplier
    /// </summary>
    public async Task<Supplier> CreateSupplier(SupplierCreateInput createDto)
    {
        var supplier = new SupplierDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            supplier.Id = createDto.Id;
        }

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SupplierDbModel>(supplier.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Supplier
    /// </summary>
    public async Task DeleteSupplier(SupplierWhereUniqueInput uniqueId)
    {
        var supplier = await _context.Suppliers.FindAsync(uniqueId.Id);
        if (supplier == null)
        {
            throw new NotFoundException();
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Suppliers
    /// </summary>
    public async Task<List<Supplier>> Suppliers(SupplierFindManyArgs findManyArgs)
    {
        var suppliers = await _context
            .Suppliers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return suppliers.ConvertAll(supplier => supplier.ToDto());
    }

    /// <summary>
    /// Meta data about Supplier records
    /// </summary>
    public async Task<MetadataDto> SuppliersMeta(SupplierFindManyArgs findManyArgs)
    {
        var count = await _context.Suppliers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Supplier
    /// </summary>
    public async Task<Supplier> Supplier(SupplierWhereUniqueInput uniqueId)
    {
        var suppliers = await this.Suppliers(
            new SupplierFindManyArgs { Where = new SupplierWhereInput { Id = uniqueId.Id } }
        );
        var supplier = suppliers.FirstOrDefault();
        if (supplier == null)
        {
            throw new NotFoundException();
        }

        return supplier;
    }

    /// <summary>
    /// Update one Supplier
    /// </summary>
    public async Task UpdateSupplier(
        SupplierWhereUniqueInput uniqueId,
        SupplierUpdateInput updateDto
    )
    {
        var supplier = updateDto.ToModel(uniqueId);

        _context.Entry(supplier).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Suppliers.Any(e => e.Id == supplier.Id))
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
