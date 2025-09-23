using CarRentalManagement.Models;


namespace CarRentalManagement.Services
{
    public interface IAuthService
    {
        object GetCurrentUser();
        Task<User?> ValidateUserAsync(string username, string password);
    }
}