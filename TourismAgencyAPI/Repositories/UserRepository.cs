using Microsoft.EntityFrameworkCore;
using TourismAgencyAPI.Data;
using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return (await context.Users.FirstOrDefaultAsync(u => u.Email == email))!;
    }

    public async Task<User?> GetByIdAsync(Guid id)  
    {
        return await context.Users.FindAsync(id);
    }

    public async Task AddAsync(User? user)
    {
        if (user != null) context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}