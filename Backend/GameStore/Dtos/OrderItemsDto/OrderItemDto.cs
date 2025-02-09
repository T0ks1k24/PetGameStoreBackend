namespace GameStore.Dtos.OrderItemsDto
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
    }
}
