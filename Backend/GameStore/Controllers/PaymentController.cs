using GameStore.Dtos.Payment;
using GameStore.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentDto paymentDto)
        {
            var success = await _paymentService.ProcessPaymentAsync(paymentDto.OrderId, paymentDto.PaymentMethod);
            if(!success) return BadRequest("Order not found or payment failed.");
            return Ok("Payment successful.");
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetPaymentStatus(int orderId)
        {
            var payment = await _paymentService.GetPaymentStatusAsync(orderId);
            if (payment == null) return NotFound("Payment not found.");
            return Ok(payment);
        }
    }
}
