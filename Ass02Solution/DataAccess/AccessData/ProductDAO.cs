using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessData
{
    public class ProductDAO : BaseDAL
    {

        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<ProductObject> GetProductList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Product";
            var product = new List<ProductObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect,
                                                        CommandType.Text,
                                                        out connection);
                while (dataReader.Read())
                {
                    product.Add(new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInstock = dataReader.GetInt32(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return product;
        }
        public ProductObject GetProductByID(int productId)
        {
            ProductObject product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Product where ProductId=@ProductId";
            try
            {
                var param = dataProvider.CreateParameter("@ProductId", 4, productId, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInstock = dataReader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return product;

        }

        public void AddNew(ProductObject product)
        {
            try
            {
                ProductObject pro = GetProductByID(product.ProductId);
                if (pro == null)
                {
                    string SQLInsert = "Insert Product values(@ProductId,@CategoryId,@ProductName,@Weight,@UnitPrice,@UnitsInstock)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 4, product.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@CategoryId", 4, product.CategoryId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ProductName", 40, product.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Weight", 20, product.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@UnitsInstock", 10, product.UnitsInstock, DbType.Int32));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public void Update(ProductObject product)
        {
            try
            {
                ProductObject p = GetProductByID(product.ProductId);
                if (p != null)
                {
                    string SQLUpdate = "Update Product set CategoryId=@CategoryId,ProductName=@ProductName,Weight=@Weight,UnitPrice=@UnitPrice,UnitsInstock=@UnitsInstock where ProductId=@ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 4, product.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@CategoryId", 4, product.CategoryId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ProductName", 40, product.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Weight", 20, product.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@UnitsInstock", 10, product.UnitsInstock, DbType.Int32));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public void Remove(int productId)
        {
            try
            {
                ProductObject product = GetProductByID(productId);
                if (product != null)
                {
                    string SQLDelete = "Delete Product where ProductId=@ProductId";
                    var param = dataProvider.CreateParameter("@ProductId", 4, product.ProductId, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }
    }
}
