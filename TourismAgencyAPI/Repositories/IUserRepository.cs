using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
}