using Microsoft.EntityFrameworkCore;
using lab3.model;

namespace lab3.api
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфігурація зв'язку між Bus і Seat
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Bus)
                .WithMany(t => t.Seats)
                .HasForeignKey(s => s.BusId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфігурація зв'язку між Seat і Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Seat)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.SeatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфігурація зв'язку між User і Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Налаштування властивостей для Booking
            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingDate)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.IsActive)
                .HasDefaultValue(true);
            
            // Конфігурація Rating
            modelBuilder.Entity<Rating>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Rating>()
                .Property(r => r.Number)
                .HasPrecision(3, 1);
        }
    }
}