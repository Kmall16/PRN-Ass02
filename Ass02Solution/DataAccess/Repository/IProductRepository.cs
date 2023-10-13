using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProducts();
        ProductObject GetProductByID(int productId);
        void InsertProduct(ProductObject product);
        void DeleteProduct(int productId);
        void UpdateProduct(ProductObject product);
    }
}
