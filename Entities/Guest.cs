using System;
using System.Collections.Generic;
using System.Text;

namespace HotelProj.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Notes { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
