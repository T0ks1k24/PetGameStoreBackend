using GameStore.Interfaces.IRepository;
using GameStore.Interfaces.IServices;
using GameStore.Models;

namespace GameStore.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Payment?> GetPaymentStatusAsync(int orderId)
    {
        return await _paymentRepository.GetByOrderIdAsync(orderId);
    }

    public async Task<bool> ProcessPaymentAsync(int orderId, string paymentMethod)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) return false;

        var payment = new Payment
        {
            OrderId = orderId,
            PaymentMethod = paymentMethod,
            IsPaid = true,
            PaidAt = DateTime.UtcNow,
        };

        await _paymentRepository.UpdateAsync(payment);
        return true;
    }
}
