using BusinessObject;
using DataAccess.AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        IEnumerable<OrderObject> IOrderRepository.GetOrders() => OrderDAO.Instance.GetOrderList();
        OrderObject IOrderRepository.GetOrderByID(int orderId) => OrderDAO.Instance.GetOrderByID(orderId);
        void IOrderRepository.InsertOrder(OrderObject order) => OrderDAO.Instance.AddNew(order);
        void IOrderRepository.DeleteOrder(int orderId) => OrderDAO.Instance.Remove(orderId);
        void IOrderRepository.UpdateOrder(OrderObject order)=> OrderDAO.Instance.Update(order);
    }
}
