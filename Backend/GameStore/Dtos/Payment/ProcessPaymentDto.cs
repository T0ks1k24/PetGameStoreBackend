﻿namespace GameStore.Dtos.Payment
{
    public class ProcessPaymentDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
