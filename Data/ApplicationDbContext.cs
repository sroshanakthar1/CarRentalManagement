using CarRentalManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<User> Users => Set<User>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();


            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Customer)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Car)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.CarID)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}