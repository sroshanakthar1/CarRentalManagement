using CarRentalManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Users> Users => Set<Users>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Booking> Bookings => Set<Booking>();


    }
}