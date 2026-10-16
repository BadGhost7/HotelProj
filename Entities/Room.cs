using System;
using System.Collections.Generic;
using System.Text;
using static HotelProj.Enums.Enums;

namespace HotelProj.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int CategoryId { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Free;
        public string? Notes { get; set; }

        public Category Category { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
