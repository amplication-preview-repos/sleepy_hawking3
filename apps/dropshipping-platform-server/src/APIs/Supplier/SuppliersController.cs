using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[ApiController()]
public class SuppliersController : SuppliersControllerBase
{
    public SuppliersController(ISuppliersService service)
        : base(service) { }
}
