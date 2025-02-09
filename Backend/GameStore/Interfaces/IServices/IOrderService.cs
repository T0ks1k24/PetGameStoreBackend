using GameStore.Dtos.Order;

namespace GameStore.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(int userId, List<int> gameIds);
        Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId);
        Task<OrderDto?> GetOrderDetailsAsync(int orderId);
    }
}
