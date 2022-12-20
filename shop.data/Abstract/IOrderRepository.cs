using shop.entity;

namespace shop.data.Abstract
{
    public interface IOrderRepository: IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}