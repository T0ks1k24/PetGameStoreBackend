namespace GameStore.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public bool IsPaid { get; set; } = false;
        public DateTime PaidAt { get; set; }

        public Order Order { get; set; }
    }
}
