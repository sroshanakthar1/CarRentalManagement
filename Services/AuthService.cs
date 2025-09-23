using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        public AuthService(ApplicationDbContext db) => _db = db;

        public object GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}