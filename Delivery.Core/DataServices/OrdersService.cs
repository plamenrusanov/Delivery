using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.ExtraItems;
using Delivery.Core.ViewModels.Orders;
using Delivery.Core.ViewModels.Rating;
using Delivery.Core.ViewModels.ShoppingCart;
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
        private readonly IRepository<ShoppingCartItem> cartItemRepo;

        public OrdersService(IRepository<Product> productRepo,
            IRepository<Extra> extraRepo,
            IRepository<Order> orderRepo,
            IRepository<DeliveryUser> userRepo,
            IRepository<ShoppingCartItem> cartItemRepo)
        {
            this.productRepo = productRepo;
            this.extraRepo = extraRepo;
            this.orderRepo = orderRepo;
            this.userRepo = userRepo;
            this.cartItemRepo = cartItemRepo;
        }

        public async Task<string> ChangeStatusAsync(string status, string orderId, string setTime, string taxId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentNullException("OrderId is null.");
            }

            if (int.TryParse(orderId, out int id))
            {
                if (Enum.TryParse(typeof(OrderStatus), status, out object? statusResult))
                {
                    try
                    {
                        var order = this.orderRepo.All().Where(x => x.Id == id).FirstOrDefault();
                        if (order is null)
                        {
                            throw new ArgumentException("Order is not exist.");
                        }

                        order.Status = (OrderStatus)statusResult!;

                        if (int.TryParse(setTime, out int minutesToDelivery))
                        {
                            order.MinutesForDelivery = minutesToDelivery;
                        }

                        switch (statusResult)
                        {
                            case OrderStatus.Processed: order.ProcessingTime = DateTime.Now; break;
                            case OrderStatus.OnDelivery: order.OnDeliveryTime = DateTime.Now; break;
                            case OrderStatus.Delivered: order.DeliveredTime = DateTime.Now; break;
                            default: break;
                        }

                        await orderRepo.SaveChangesAsync();

                        return order.DeliveryUserId;
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }

                throw new ArgumentException("Status is not valid.");
            }
            else
            {
                throw new ArgumentException("OrderId is not valid integer.");
            }
        }

        public async Task<int> CreateOrderAsync(OrderInputModel model, DeliveryUser user)
        {
            Order order = new()
            {
                Name = model.Username,
                Phone = model.Phone,
                AddInfo = model.AddInfoOrder ?? String.Empty,
                DeliveryUserId = user.Id,
                CreatedOn = DateTime.Now,
                Cutlery = model.CutleryCount,
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

            return order.Id;
        }

        public Task<List<OrderViewModel>> GetDailyOrdersAsync()
        {
            return orderRepo
                .All()
                .Where(x => x.CreatedOn.Date == DateTime.Now.Date)
               .OrderByDescending(x => x.Id)
               .Select(x => new OrderViewModel()
               {
                   OrderId = x.Id,
                   Status = x.Status.ToString(),
               }).ToListAsync();
        }

        public async Task<OrderDetailsViewModel> GetDetailsByIdAsync(string orderId)
        {
            if (!int.TryParse(orderId, out int id))
            {
                throw new ArgumentException("Order not exists.");
            }

            var order = await orderRepo.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (order is null)
            {
                throw new ArgumentException("Order not exists.");
            }

            var model = new OrderDetailsViewModel()
            {
                Latitude = order.Address?.Latitude,
                Longitude = order.Address?.Longitude,
                CreatedOn = order.CreatedOn.ToString("dd/MM/yy HH:mm"),
                OrderId = order.Id,
                DisplayAddress = order.Address?.ToString(),
                AddressInfo = order.Address?.AddInfo,
                UserUserName = order.Name,
                UserPhone = order.Phone,
                AddInfo = order.AddInfo,
                CutleryCount = order.Cutlery,
                CustomerComment = order.CustomerComment,
                CartItems = order.CartItems
                    .Select(x => new ShoppingItemsViewModel()
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        ProductPrice = x.Product.Price,
                        Quantity = x.Quantity,
                        Description = x.Description,
                        Rating = x.Rating,
                        Extras = x.ExtraItems
                                  .Select(e => new ExtraCartItemModel()
                                  {
                                      Name = e.Extra.Name,
                                      Price = e.Extra.Price,
                                      Quantity = e.Quantity,
                                  }).ToList(),
                    }).ToList(),
                Status = order.Status,
                PackagesPrice = order.CartItems.Sum(x => (decimal)Math.Ceiling(x.Quantity / (double)x.Product.MaxProductsInPackage) * x.Product.Package.Price),
            };
            model.TotalPrice = model.CartItems.Sum(x => x.ItemPrice) + model.PackagesPrice;


            if (model.Status != OrderStatus.Unprocessed)
            {
                model.TimeForDelivery = order.ProcessingTime.HasValue ? order.ProcessingTime.Value.AddMinutes((double)order.MinutesForDelivery).ToString("dd/MM/yy HH:mm") : String.Empty;
            }

            return model;
        }

        public async Task<IEnumerable<OrderViewModel>> GetMyOrdersAsync(DeliveryUser? user)
        {
            if (user is null)
            {
                return new List<OrderViewModel>();
            }

            return await this.orderRepo
                .All()
                .Where(x => x.DeliveryUserId == user.Id)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new OrderViewModel()
                {
                    OrderId = x.Id,
                    Status = x.Status.ToString(),
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                }).Take(10).ToListAsync();
        }

        public async Task<UserOrderDetailsViewModel> GetUserDetailsByIdAsync(int id)
        {
            var order = await orderRepo.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (order is null)
            {
                throw new ArgumentException("Order not exists.");
            }

            var model = new UserOrderDetailsViewModel()
            {
                CreatedOn = order.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                OrderId = order.Id,
                CutleryCount = order.Cutlery,
                CustomerComment = order.CustomerComment,
                CartItems = order.CartItems
                    .Select(x => new ShoppingItemsViewModel()
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        ProductPrice = x.Product.Price,
                        Quantity = x.Quantity,
                        Description = x.Description,
                        Rating = x.Rating,
                        Extras = x.ExtraItems
                                  .Select(e => new ExtraCartItemModel()
                                  {
                                      Name = e.Extra.Name,
                                      Price = e.Extra.Price,
                                      Quantity = e.Quantity,
                                  }).ToList(),
                    }).ToList(),
                Status = order.Status,
                PackagesPrice = order.CartItems.Sum(x => (decimal)Math.Ceiling(x.Quantity / (double)x.Product.MaxProductsInPackage) * x.Product.Package.Price),
            };

            model.TotalPrice = model.CartItems.Sum(x => x.ItemPrice) + model.PackagesPrice;



            if (model.Status != OrderStatus.Unprocessed)
            {
                model.TimeForDelivery = order.ProcessingTime.HasValue
                    ? order.ProcessingTime.Value.AddMinutes((double)order.MinutesForDelivery).ToString("dd/MM/yyyy HH:mm")
                    : String.Empty;
            }

            return model;
        }

        public async Task SetRatingAsync(List<RatingItemDto> ratings, string message)
        {
            if (ratings.Count > 0)
            {
                var order = this.orderRepo
                    .All()
                    .Where(x => x.CartItems.Any(i => i.Id.ToString() == ratings.First().ItemId))
                    .FirstOrDefault();
                if (order is null)
                {
                    throw new ArgumentException("Order not exist!");
                }

                order.CustomerComment = message;
                this.orderRepo.Update(order);

                foreach (var item in ratings)
                {
                    if (byte.TryParse(item.Rating, out byte result) && int.TryParse(item.ItemId, out int itemId))
                    {
                        var shopItem = cartItemRepo
                            .All()
                            .Where(x => x.Id == itemId)
                            .FirstOrDefault();

                        if (shopItem is null || shopItem.Rating != default)
                        {
                            throw new Exception("Rating is already set!");
                        }

                        shopItem.Rating = result;
                        cartItemRepo.Update(shopItem);
                    }
                }
                await this.orderRepo.SaveChangesAsync();
            }
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
            catch (Exception)
            { return false; }

        }


    }
}
