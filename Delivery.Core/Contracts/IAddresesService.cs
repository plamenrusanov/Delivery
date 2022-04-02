using Delivery.Core.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Contracts
{
    public interface IAddresesService
    {
        Task<AddressInputModel> GetAddressAsync(string latitude, string longitude);
    }
}
