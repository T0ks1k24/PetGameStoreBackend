namespace GameStore.Dtos.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public List<int> GameIds { get; set; }
    }
}
