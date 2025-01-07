using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[ApiController()]
public class MarketplacesController : MarketplacesControllerBase
{
    public MarketplacesController(IMarketplacesService service)
        : base(service) { }
}
