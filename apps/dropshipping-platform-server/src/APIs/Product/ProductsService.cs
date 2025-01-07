using DropshippingPlatform.Infrastructure;

namespace DropshippingPlatform.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(DropshippingPlatformDbContext context)
        : base(context) { }
}
