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
    public class OrderDetailDAO : BaseDAL
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<OrderDetailObject> GetOrderDetailList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from OrderDetail";
            var order = new List<OrderDetailObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect,
                                                        CommandType.Text,
                                                        out connection);
                while (dataReader.Read())
                {
                    order.Add(new OrderDetailObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        ProductId = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetFloat(4)
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
            return order;
        }
        public OrderDetailObject GetOrderDetailByID(int orderId, int productId)
        {
            OrderDetailObject orderDetail = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select * from OrderDetail where OrderId=@OrderId and ProductId=@ProductId";
            try
            {
                var param1 = dataProvider.CreateParameter("@OrderId", 4, orderId, DbType.Int32);
                var param2 = dataProvider.CreateParameter("@ProductId", 4, productId, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param1, param2);
                if (dataReader.Read())
                {
                    orderDetail = new OrderDetailObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        ProductId = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetFloat(4)
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
            return orderDetail;

        }

        public void AddNew(OrderDetailObject orderDetail)
        {
            try
            {
                OrderDetailObject pro = GetOrderDetailByID(orderDetail.OrderId, orderDetail.ProductId);
                if (pro == null)
                {
                    string SQLInsert = "Insert OrderDetail values(@OrderId,@ProductId,@UnitPrice,@Quantity,@Discount)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@OrderId", 4, orderDetail.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 4, orderDetail.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 50, orderDetail.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@Quantity", 10, orderDetail.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Discount", 15, orderDetail.Discount, DbType.Single));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The order detail is already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public void Update(OrderDetailObject orderDetail)
        {
            try
            {
                OrderDetailObject m = GetOrderDetailByID(orderDetail.OrderId, orderDetail.ProductId);
                if (m != null)
                {
                    string SQLUpdate = "Update OrderDetail set UnitPrice=@UnitPrice,Quantity=@Quantity,Discount=@Discount where OrderId=@OrderId and ProductId=@ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@OrderId", 4, orderDetail.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 4, orderDetail.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 50, orderDetail.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter("@Quantity", 10, orderDetail.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Discount", 15, orderDetail.Discount, DbType.Single));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The order detail does not already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public void Remove(int orderId, int productId)
        {
            try
            {
                OrderDetailObject m = GetOrderDetailByID(orderId, productId);
                if (m != null)
                {
                    string SQLDelete = "Delete OrderDetail where OrderId=@OrderId and ProductId=@ProductId";
                    var param1 = dataProvider.CreateParameter("@OrderId", 4, orderId, DbType.Int32);
                    var param2 = dataProvider.CreateParameter("@ProductId", 4, productId, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param1, param2);
                }
                else
                {
                    throw new Exception("The order detail does not already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }


    }
}

