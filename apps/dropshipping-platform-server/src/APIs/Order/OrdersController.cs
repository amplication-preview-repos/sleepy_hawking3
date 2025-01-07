using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
