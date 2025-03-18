using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(User? user, string password);
    Task<string> LoginAsync(string email, string password);
}