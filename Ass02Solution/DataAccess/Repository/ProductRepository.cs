using BusinessObject;
using DataAccess.AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        IEnumerable<ProductObject> IProductRepository.GetProducts() => ProductDAO.Instance.GetProductList();
        ProductObject IProductRepository.GetProductByID(int productId) => ProductDAO.Instance.GetProductByID(productId);
        void IProductRepository.InsertProduct(ProductObject product) => ProductDAO.Instance.AddNew(product);
        void IProductRepository.DeleteProduct(int productId) => ProductDAO.Instance.Remove(productId);
        void IProductRepository.UpdateProduct(ProductObject product) => ProductDAO.Instance.Update(product);
    }
}
