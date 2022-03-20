using Delivery.Core.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Contracts
{
    public interface IProductService
    {
        Task<ProductInputModel> AddDropdownsCollections(ProductInputModel model);
        Task CreateProductAsync(ProductInputModel model);
    }
}
