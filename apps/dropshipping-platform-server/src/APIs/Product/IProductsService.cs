using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;

namespace DropshippingPlatform.APIs;

public interface IProductsService
{
    /// <summary>
    /// Create one Product
    /// </summary>
    public Task<Product> CreateProduct(ProductCreateInput product);

    /// <summary>
    /// Delete one Product
    /// </summary>
    public Task DeleteProduct(ProductWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Products
    /// </summary>
    public Task<List<Product>> Products(ProductFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    public Task<MetadataDto> ProductsMeta(ProductFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Product
    /// </summary>
    public Task<Product> Product(ProductWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Product
    /// </summary>
    public Task UpdateProduct(ProductWhereUniqueInput uniqueId, ProductUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Orders records to Product
    /// </summary>
    public Task ConnectOrders(ProductWhereUniqueInput uniqueId, OrderWhereUniqueInput[] ordersId);

    /// <summary>
    /// Disconnect multiple Orders records from Product
    /// </summary>
    public Task DisconnectOrders(
        ProductWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] ordersId
    );

    /// <summary>
    /// Find multiple Orders records for Product
    /// </summary>
    public Task<List<Order>> FindOrders(
        ProductWhereUniqueInput uniqueId,
        OrderFindManyArgs OrderFindManyArgs
    );

    /// <summary>
    /// Update multiple Orders records for Product
    /// </summary>
    public Task UpdateOrders(ProductWhereUniqueInput uniqueId, OrderWhereUniqueInput[] ordersId);
}
