using DropshippingPlatform.Infrastructure;

namespace DropshippingPlatform.APIs;

public class MarketplacesService : MarketplacesServiceBase
{
    public MarketplacesService(DropshippingPlatformDbContext context)
        : base(context) { }
}
