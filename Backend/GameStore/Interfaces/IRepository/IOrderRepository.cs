using GameStore.Models;

namespace GameStore.Interfaces.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task<Order> CreateAsync(Order order);
    }
}
