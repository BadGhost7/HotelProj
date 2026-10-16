using System;
using System.Collections.Generic;
using System.Text;
using static HotelProj.Enums.Enums;
using HotelProj.Enums;

namespace HotelProj.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Receptionist;

        public ICollection<Booking> CreatedBookings { get; set; } = new List<Booking>();
        public ICollection<Payment> AcceptedPayments { get; set; } = new List<Payment>();
    }
}
