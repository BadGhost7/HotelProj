using System;
using System.Collections.Generic;
using System.Text;
using static HotelProj.Enums.Enums;

namespace HotelProj.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
        public PaymentMethod Method { get; set; }
        public int AcceptedBy { get; set; }

        public Booking Booking { get; set; } = null!;
        public User Acceptor { get; set; } = null!;
    }
}
