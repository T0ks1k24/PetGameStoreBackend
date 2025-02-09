using GameStore.Models;

namespace GameStore.Interfaces.IServices
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(int orderId, string paymentMethod);
        Task<Payment?> GetPaymentStatusAsync(int orderId);
    }
}
