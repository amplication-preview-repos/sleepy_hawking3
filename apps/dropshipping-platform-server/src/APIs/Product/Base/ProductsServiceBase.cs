using DropshippingPlatform.APIs;
using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.APIs.Dtos;
using DropshippingPlatform.APIs.Errors;
using DropshippingPlatform.APIs.Extensions;
using DropshippingPlatform.Infrastructure;
using DropshippingPlatform.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DropshippingPlatform.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly DropshippingPlatformDbContext _context;

    public ProductsServiceBase(DropshippingPlatformDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    public async Task<Product> CreateProduct(ProductCreateInput createDto)
    {
        var product = new ProductDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            product.Id = createDto.Id;
        }
        if (createDto.Orders != null)
        {
            product.Orders = await _context
                .Orders.Where(order => createDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProductDbModel>(product.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    public async Task DeleteProduct(ProductWhereUniqueInput uniqueId)
    {
        var product = await _context.Products.FindAsync(uniqueId.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    public async Task<List<Product>> Products(ProductFindManyArgs findManyArgs)
    {
        var products = await _context
            .Products.Include(x => x.Orders)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return products.ConvertAll(product => product.ToDto());
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    public async Task<MetadataDto> ProductsMeta(ProductFindManyArgs findManyArgs)
    {
        var count = await _context.Products.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    public async Task<Product> Product(ProductWhereUniqueInput uniqueId)
    {
        var products = await this.Products(
            new ProductFindManyArgs { Where = new ProductWhereInput { Id = uniqueId.Id } }
        );
        var product = products.FirstOrDefault();
        if (product == null)
        {
            throw new NotFoundException();
        }

        return product;
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    public async Task UpdateProduct(ProductWhereUniqueInput uniqueId, ProductUpdateInput updateDto)
    {
        var product = updateDto.ToModel(uniqueId);

        if (updateDto.Orders != null)
        {
            product.Orders = await _context
                .Orders.Where(order => updateDto.Orders.Select(t => t).Contains(order.Id))
                .ToListAsync();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == product.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Orders records to Product
    /// </summary>
    public async Task ConnectOrders(
        ProductWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Products.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Orders);

        foreach (var child in childrenToConnect)
        {
            parent.Orders.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Orders records from Product
    /// </summary>
    public async Task DisconnectOrders(
        ProductWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Products.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Orders?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Orders records for Product
    /// </summary>
    public async Task<List<Order>> FindOrders(
        ProductWhereUniqueInput uniqueId,
        OrderFindManyArgs productFindManyArgs
    )
    {
        var orders = await _context
            .Orders.Where(m => m.ProductId == uniqueId.Id)
            .ApplyWhere(productFindManyArgs.Where)
            .ApplySkip(productFindManyArgs.Skip)
            .ApplyTake(productFindManyArgs.Take)
            .ApplyOrderBy(productFindManyArgs.SortBy)
            .ToListAsync();

        return orders.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Orders records for Product
    /// </summary>
    public async Task UpdateOrders(
        ProductWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var product = await _context
            .Products.Include(t => t.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        product.Orders = children;
        await _context.SaveChangesAsync();
    }
}
