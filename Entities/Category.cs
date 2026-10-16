using System;
using System.Collections.Generic;
using System.Text;

namespace HotelProj.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
