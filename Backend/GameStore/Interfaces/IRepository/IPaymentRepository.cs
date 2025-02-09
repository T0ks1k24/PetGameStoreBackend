using GameStore.Models;

namespace GameStore.Interfaces.IRepository;

public interface IPaymentRepository
{
    Task<Payment?> GetByOrderIdAsync(int orderId);
    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
}
