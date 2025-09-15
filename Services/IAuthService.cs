using CarRentalManagement.Models;


namespace CarRentalManagement.Services
{
    public interface IAuthService
    {
        Task<Users?> ValidateUserAsync(string username, string password);
    }
}