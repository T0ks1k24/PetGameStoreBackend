namespace GameStore.Dtos.OrderItemsDto
{
    public class AddOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public decimal Price { get; set; }
    }
}
