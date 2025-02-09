using GameStore.Dtos.OrderItemsDto;
using GameStore.Models;

namespace GameStore.Dtos.Order;

public class OrderDto
{
    public int Id { get; set; }
    public string UserName {  get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}
