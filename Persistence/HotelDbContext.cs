using HotelProj.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using HotelProj.Enums;


namespace HotelProj.Persistence
{
    public class HotelDbContext : DbContext
    {
        
        public HotelDbContext() { }
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=hotel.db");
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           


            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("users");
                e.HasKey(u => u.Id);
                e.Property(u => u.Login).IsRequired().HasMaxLength(64);
                e.Property(u => u.Password).IsRequired().HasMaxLength(255);
                e.Property(u => u.FullName).IsRequired().HasMaxLength(200);
                e.Property(u => u.Role).HasConversion<string>()
                    .HasDefaultValue(UserRole.Receptionist).IsRequired();
                e.HasIndex(u => u.Login).IsUnique();
                e.ToTable(t => t.HasCheckConstraint("ck_users_role",
                    "Role IN ('Admin','Receptionist')"));
            });




          
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("categories");
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.BasePrice).HasColumnType("decimal(10,2)").IsRequired();
                e.HasIndex(c => c.Name).IsUnique();
                e.ToTable(t => t.HasCheckConstraint("ck_categories_base_price",
                    "BasePrice >= 0"));
            });




          
            modelBuilder.Entity<Room>(e =>
            {
                e.ToTable("rooms");
                e.HasKey(r => r.Id);
                e.Property(r => r.Number).IsRequired().HasMaxLength(20);
                e.Property(r => r.PricePerNight).HasColumnType("decimal(10,2)").IsRequired();
                e.Property(r => r.Status).HasConversion<string>()
                    .HasDefaultValue(RoomStatus.Free).IsRequired();
                e.HasIndex(r => r.Number).IsUnique();

                e.HasOne(r => r.Category).WithMany(c => c.Rooms)
                    .HasForeignKey(r => r.CategoryId).OnDelete(DeleteBehavior.Restrict);

                e.ToTable(t =>
                {
                    t.HasCheckConstraint("ck_rooms_capacity", "Capacity >= 1");
                    t.HasCheckConstraint("ck_rooms_price", "PricePerNight >= 0");
                    t.HasCheckConstraint("ck_rooms_status", "Status IN ('Free','Occupied','Repair')");
                });
            });

         


            modelBuilder.Entity<Guest>(e =>
            {
                e.ToTable("guests");
                e.HasKey(g => g.Id);
                e.Property(g => g.FullName).IsRequired().HasMaxLength(200);
                e.Property(g => g.Passport).IsRequired().HasMaxLength(20);
                e.Property(g => g.Phone).IsRequired().HasMaxLength(20);
                e.Property(g => g.Email).IsRequired().HasMaxLength(100);
                e.HasIndex(g => g.Passport).IsUnique();
                e.HasIndex(g => g.Email).IsUnique();
            });




    
            modelBuilder.Entity<Booking>(e =>
            {
                e.ToTable("bookings");
                e.HasKey(b => b.Id);
                e.Property(b => b.Status).HasConversion<string>()
                    .HasDefaultValue(BookingStatus.Booked).IsRequired();
                e.Property(b => b.TotalPrice).HasColumnType("decimal(10,2)").HasDefaultValue(0m).IsRequired();
                e.Property(b => b.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

                e.HasOne(b => b.Guest).WithMany(g => g.Bookings)
                    .HasForeignKey(b => b.GuestId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(b => b.Room).WithMany(r => r.Bookings)
                    .HasForeignKey(b => b.RoomId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(b => b.Creator).WithMany(u => u.CreatedBookings)
                    .HasForeignKey(b => b.CreatedBy).OnDelete(DeleteBehavior.Restrict);

                e.ToTable(t =>
                {
                    t.HasCheckConstraint("ck_bookings_checkout", "CheckOut > CheckIn");
                    t.HasCheckConstraint("ck_bookings_total_price", "TotalPrice >= 0");
                    t.HasCheckConstraint("ck_bookings_status",
                        "Status IN ('Booked','CheckedIn','CheckedOut','Cancelled')");
                });
            });

         


            modelBuilder.Entity<Payment>(e =>
            {
                e.ToTable("payments");
                e.HasKey(p => p.Id);
                e.Property(p => p.Amount).HasColumnType("decimal(10,2)").IsRequired();
                e.Property(p => p.PaidAt).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
                e.Property(p => p.Method).HasConversion<string>().IsRequired();

                e.HasOne(p => p.Booking).WithMany(b => b.Payments)
                    .HasForeignKey(p => p.BookingId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Acceptor).WithMany(u => u.AcceptedPayments)
                    .HasForeignKey(p => p.AcceptedBy).OnDelete(DeleteBehavior.Restrict);

                e.ToTable(t =>
                {
                    t.HasCheckConstraint("ck_payments_amount", "Amount > 0");
                    t.HasCheckConstraint("ck_payments_method", "Method IN ('Cash','Card')");
                });
            });
        }
    }
}
