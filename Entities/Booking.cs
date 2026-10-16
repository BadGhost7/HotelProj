using System;
using System.Collections.Generic;
using System.Text;
using static HotelProj.Enums.Enums;

namespace HotelProj.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Booked;
        public decimal TotalPrice { get; set; } = 0m;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guest Guest { get; set; } = null!;
        public Room Room { get; set; } = null!;
        public User Creator { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
