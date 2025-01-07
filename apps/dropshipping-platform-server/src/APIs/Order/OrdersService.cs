using DropshippingPlatform.Infrastructure;

namespace DropshippingPlatform.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(DropshippingPlatformDbContext context)
        : base(context) { }
}
