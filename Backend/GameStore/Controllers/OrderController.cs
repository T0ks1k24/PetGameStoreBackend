using GameStore.Dtos.Order;
using GameStore.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders([FromRoute] int userId)
        {
            var order = _orderService.GetOrdersAsync(userId);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var order = _orderService.GetOrderDetailsAsync(id);
            return Ok(order);
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreateOrder([FromRoute] CreateOrderDto orderDto)
        {
            if (orderDto == null || orderDto.GameIds == null || !orderDto.GameIds.Any())
            {
                return BadRequest(new { message = "Некоректні дані для створення замовлення" });
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(orderDto.UserId, orderDto.GameIds);
                return CreatedAtAction(nameof(GetOrderById), new { orderId = order.Id }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
