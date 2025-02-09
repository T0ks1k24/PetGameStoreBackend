using GameStore.Interfaces.IRepository;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payment.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payment.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payment.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
