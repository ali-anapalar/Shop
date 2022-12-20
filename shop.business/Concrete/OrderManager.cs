using shop.business.Abstract;
using shop.data.Abstract;
using shop.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.business.Concrete
{
    public class OrderManager:IOrderService

    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void Create(Order entity)
        {
            _orderRepository.Create(entity);
        }

        public List<Order> GetOrders(string userId)
        {
            return _orderRepository.GetOrders(userId);
        }
    }
}
