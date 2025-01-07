using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;

namespace DropshippingPlatform.APIs;

public interface ISuppliersService
{
    /// <summary>
    /// Create one Supplier
    /// </summary>
    public Task<Supplier> CreateSupplier(SupplierCreateInput supplier);

    /// <summary>
    /// Delete one Supplier
    /// </summary>
    public Task DeleteSupplier(SupplierWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Suppliers
    /// </summary>
    public Task<List<Supplier>> Suppliers(SupplierFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Supplier records
    /// </summary>
    public Task<MetadataDto> SuppliersMeta(SupplierFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Supplier
    /// </summary>
    public Task<Supplier> Supplier(SupplierWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Supplier
    /// </summary>
    public Task UpdateSupplier(SupplierWhereUniqueInput uniqueId, SupplierUpdateInput updateDto);
}
