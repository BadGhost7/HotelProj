using System;
using System.Linq;
using HotelProj.Entities;
using HotelProj.Enums;
using HotelProj.Persistence;

namespace HotelProj.Services
{
    public class BookingService{
        public void CreatingBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut, int adminId){
            using var db = new HotelDbContext();
            bool isOcupied = db.Bookings.Any(b => b.RoomId == roomId && b.Status != BookingStatus.Cancelled && b.CheckIn < checkOut && b.CheckOut > checkIn);
            if (isOcupied)
                throw new Exception("Ошибка: Номер занят на выбранные даты")
            var room = db.Rooms.Find(roomId);
            int days = (checkOut - checkIn).Days;
            decimal total = days * room.PricePerNight;

            var booking = new Booking{
                GuestId = guestId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = BookingStatus.Booked,
                TotalPrice = total,
                CreatedBy = adminId
            };
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
    }
}