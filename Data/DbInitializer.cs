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
                new Car { CarName = "Wagon R", CarModel = "2024", ImageUrl = "https://preview.redd.it/suzuki-wagon-r-worth-it-for-6-mn-v0-2a8a1kk52klc1.jpeg?auto=webp&s=ee5fc464ce6269bfda674f7ef45c67a7a7e7ce7b", IsAvailable = true , Transmission = "Automatic"},
                new Car { CarName = "Suzuki Every", CarModel = "2023", ImageUrl = "https://www.saleme.lk/cdn/saleme/images/uploads/84734/saleme_61adf237aa0c5.jpg", IsAvailable = true, Transmission = "Automatic" },
                new Car { CarName = "Toyota Prius", CarModel = "2022", ImageUrl = "https://di-uploads-pod10.dealerinspire.com/toyotaofcedarpark/uploads/2019/11/2020-Prius-Blue-1.jpg", IsAvailable = true, Transmission = "Automatic" }
                );
            }


            await db.SaveChangesAsync();
        }
    }
}