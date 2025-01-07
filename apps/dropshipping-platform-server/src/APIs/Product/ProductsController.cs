using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
