using DropshippingPlatform.APIs.Common;
using DropshippingPlatform.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DropshippingPlatform.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class MarketplaceFindManyArgs : FindManyInput<Marketplace, MarketplaceWhereInput> { }
