using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Orders;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Models.Enums;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<Extra> extraRepo;
        private readonly IRepository<Order> orderRepo;
        private readonly IRepository<DeliveryUser> userRepo;

        public OrdersService(IRepository<Product> productRepo,
            IRepository<Extra> extraRepo,
            IRepository<Order> orderRepo,
            IRepository<DeliveryUser> userRepo)
        {
            this.productRepo = productRepo;
            this.extraRepo = extraRepo;
            this.orderRepo = orderRepo;
            this.userRepo = userRepo;
        }

        public async Task CreateOrderAsync(OrderInputModel model, DeliveryUser user)
        {
            Order order = new ()
            {
                Name = model.Username,
                Phone = model.Phone,
                AddInfo = model.AddInfoOrder ?? String.Empty,
                DeliveryUserId = user.Id,
                CreatedOn = DateTime.Now,
                Address = new DeliveryAddress()
                {
                    AddInfo = model.Address.addInfo,
                    City = model.Address.city,
                    Borough = model.Address.borough,
                    Street = model.Address.street,
                    StreetNumber = model.Address.streetNumber,
                    Block = model.Address.block,
                    Floor = model.Address.floor,
                    DeliveryUserId = user.Id,
                    DisplayName = model.Address.displayName,
                    Entry = model.Address.entry,
                    Latitude = model.Address.latitude,
                    Longitude = model.Address.longitude,
                    CreatedOn = DateTime.Now,
                },
                Status = OrderStatus.Unprocessed,
                CartItems = model.Cart.Select(x => new ShoppingCartItem()
                {
                    ProductId = x.PId,
                    Quantity = x.Qty,
                    Description = x.Description ?? String.Empty,
                    CreatedOn = DateTime.Now,
                    ExtraItems = x.Extras.Select(e => new ExtraItem()
                    {
                        ExtraId = e.id,
                        Quantity = e.qty,
                    }).ToList()
                }).ToList()
            };

            user.Orders.Add(order);

            await userRepo.SaveChangesAsync();
        }

        public async Task<bool> ValidateWithMirrorObjectAsync(OrderInputModel model)
        {
            try
            {
                var eid = model.Cart.SelectMany(s => s.Extras).Select(s => s.id);

                var extras = await extraRepo
                    .All()
                    .Where(x => !x.IsDeleted)
                    .Where(x => eid.Any(a => a == x.Id))
                    .ToListAsync();

                var pid = model.Cart.Select(s => s.PId);

                var products = await productRepo
                    .All()
                    .Where(x => !x.IsDeleted)
                    .Where(x => pid.Any(a => a == x.Id))
                    .ToListAsync();

                foreach (var p in model.Cart)
                {
                    var product = products.FirstOrDefault(x => x.Id == p.PId);
                    if (product is null) return false;

                    if (p.ProductPrice != product!.Price) return false;

                    if (p.PName != product.Name) return false;

                    if (p.PackagePrice != product.Package.Price) return false;

                    var tempSum = (p.ProductPrice * p.Qty) + (Math.Ceiling(p.Qty / Convert.ToDecimal(p.MaxProducts)) * p.PackagePrice);

                    foreach (var e in p.Extras)
                    {
                        var extra = extras.FirstOrDefault(x => x.Id == e.id);

                        if (extra is null) return false;

                        if (e.name != extra!.Name) return false;

                        tempSum += e.qty * extra!.Price;
                    }

                    if (tempSum != p.SubTotal) return false;

                }
                return true;
            }
            catch (Exception e)
            { return false; }

        }
    }
}
