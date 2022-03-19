using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.DataServices
{
    public class ProductService : IProductService
    {
        public ProductService()
        {

        }

        public Task<ProductInputModel> AddDropdownsCollections(ProductInputModel model)
        {
            //throw new NotImplementedException();
            return Task.FromResult(model);
        }
    }
}
