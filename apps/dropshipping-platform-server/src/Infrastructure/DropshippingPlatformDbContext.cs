using DropshippingPlatform.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DropshippingPlatform.Infrastructure;

public class DropshippingPlatformDbContext : DbContext
{
    public DropshippingPlatformDbContext(DbContextOptions<DropshippingPlatformDbContext> options)
        : base(options) { }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<MarketplaceDbModel> Marketplaces { get; set; }

    public DbSet<SupplierDbModel> Suppliers { get; set; }

    public DbSet<ProductDbModel> Products { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
