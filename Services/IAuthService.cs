using CarRentalManagement.Models;


namespace CarRentalManagement.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string username, string password);
    }
}