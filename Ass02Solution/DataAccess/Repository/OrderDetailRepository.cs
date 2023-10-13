using BusinessObject;
using DataAccess.AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        IEnumerable<OrderDetailObject> IOrderDetailRepository.GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailList();
        OrderDetailObject IOrderDetailRepository.GetOrderDetailByID(int orderId, int productId) => OrderDetailDAO.Instance.GetOrderDetailByID(orderId, productId);
        void IOrderDetailRepository.InsertOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.AddNew(orderDetail);
        void IOrderDetailRepository.DeleteOrderDetail(int orderId, int productId) => OrderDetailDAO.Instance.Remove(orderId, productId);
        void IOrderDetailRepository.UpdateOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);
    }
}
