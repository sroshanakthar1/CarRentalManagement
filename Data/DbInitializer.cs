using CarRentalManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            await db.Database.EnsureCreatedAsync();


            if (!await db.Users.AnyAsync())
            {
                db.Users.AddRange(
                new User { Username = "admin", Password = "admin123", Role = "Admin" },
                new User { Username = "alice", Password = "password", Role = "Customer" }
                );
            }


            if (!await db.Cars.AnyAsync())
            {
                db.Cars.AddRange(
                new Car { CarName = "Honda Civic", CarModel = "2024", ImageUrl = "https://via.placeholder.com/400x250?text=Civic", IsAvailable = true , Transmission = "Automatic"},
                new Car { CarName = "Toyota Corolla", CarModel = "2023", ImageUrl = "https://via.placeholder.com/400x250?text=Corolla", IsAvailable = true, Transmission = "Automatic" },
                new Car { CarName = "Nissan Leaf", CarModel = "2022", ImageUrl = "https://via.placeholder.com/400x250?text=Leaf", IsAvailable = true, Transmission = "Automatic" }
                );
            }


            await db.SaveChangesAsync();
        }
    }
}