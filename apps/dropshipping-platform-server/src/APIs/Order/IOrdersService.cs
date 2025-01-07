using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;

namespace DropshippingPlatform.APIs;

public interface IOrdersService
{
    /// <summary>
    /// Create one Order
    /// </summary>
    public Task<Order> CreateOrder(OrderCreateInput order);

    /// <summary>
    /// Delete one Order
    /// </summary>
    public Task DeleteOrder(OrderWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Orders
    /// </summary>
    public Task<List<Order>> Orders(OrderFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    public Task<MetadataDto> OrdersMeta(OrderFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Order
    /// </summary>
    public Task<Order> Order(OrderWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Order
    /// </summary>
    public Task UpdateOrder(OrderWhereUniqueInput uniqueId, OrderUpdateInput updateDto);

    /// <summary>
    /// Connect multiple product records to Order
    /// </summary>
    public Task ConnectProduct(
        OrderWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] productsId
    );

    /// <summary>
    /// Disconnect multiple product records from Order
    /// </summary>
    public Task DisconnectProduct(
        OrderWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] productsId
    );

    /// <summary>
    /// Find multiple product records for Order
    /// </summary>
    public Task<List<Product>> FindProduct(
        OrderWhereUniqueInput uniqueId,
        ProductFindManyArgs ProductFindManyArgs
    );

    /// <summary>
    /// Update multiple product records for Order
    /// </summary>
    public Task UpdateProduct(OrderWhereUniqueInput uniqueId, ProductWhereUniqueInput[] productsId);
}
