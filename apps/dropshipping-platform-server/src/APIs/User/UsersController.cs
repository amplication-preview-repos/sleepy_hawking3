using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
