using DropshippingPlatform.APIs;

namespace DropshippingPlatform;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMarketplacesService, MarketplacesService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<ISuppliersService, SuppliersService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
