using DropshippingPlatform.Infrastructure;

namespace DropshippingPlatform.APIs;

public class SuppliersService : SuppliersServiceBase
{
    public SuppliersService(DropshippingPlatformDbContext context)
        : base(context) { }
}
