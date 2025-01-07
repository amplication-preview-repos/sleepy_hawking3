using DropshippingPlatform.Infrastructure;

namespace DropshippingPlatform.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(DropshippingPlatformDbContext context)
        : base(context) { }
}
